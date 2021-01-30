// Player.cs
using Godot;
using System;

public class Player : KinematicBody
{
	[Export]
	public float MaxSpeed = 5.0f;
	[Export]
	public float Acceleration = 1.0f;
	[Export]
	public float Deacceleration = 3.0f;
	[Export]
	public float MaxSlopeAngle = 45.0f;
	[Export]
	public float MouseSensitivity = 0.05f;

	[Export]
	public Vector3 velocity = new Vector3();

	[Export]
	public int IdleWarmthLoss = 2;
	[Export]
	public int MovingWarmthLoss = 1;

	private const float MAX_WARMTH = 100;
	private Vector3 _direction = new Vector3();
	private float _warmth = MAX_WARMTH;

	private Camera _camera;
	private Spatial _rotationPivot;
	private ColdOverlay _coldOverlay;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_camera = GetNode<Camera>("RotationPivot/Camera");
		_rotationPivot = GetNode<Spatial>("RotationPivot");
		_coldOverlay = GetNode<ColdOverlay>("RotationPivot/Camera/CanvasLayer/ColdOverlay");
		Input.SetMouseMode(Input.MouseMode.Captured);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion && Input.GetMouseMode() == Input.MouseMode.Captured)
		{
			InputEventMouseMotion mouseEvent = @event as InputEventMouseMotion;
			_rotationPivot.RotateX(Mathf.Deg2Rad(-mouseEvent.Relative.y * MouseSensitivity));
			RotateY(Mathf.Deg2Rad(-mouseEvent.Relative.x * MouseSensitivity));

			Vector3 cameraRotation = _rotationPivot.RotationDegrees;
			cameraRotation.x = Mathf.Clamp(cameraRotation.x, -70, 70);
			_rotationPivot.RotationDegrees = cameraRotation;
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		ProcessInput(delta);
		ProcessMovement(delta);
		ProcessWarmth(delta);
	}

	private void ProcessInput(float delta)
	{
		_direction = new Vector3();
		Transform camXform = _camera.GlobalTransform;
		Vector2 inputMovementVector = new Vector2(); // x maps to x, y maps to z

		if (Input.IsActionPressed("ui_right"))
		{
			inputMovementVector.x += 1;
		}
		if (Input.IsActionPressed("ui_left"))
		{
			inputMovementVector.x -= 1;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			inputMovementVector.y += 1;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			inputMovementVector.y -= 1;
		}

		inputMovementVector = inputMovementVector.Normalized();
		_direction += camXform.basis.x * inputMovementVector.x;
		_direction += -camXform.basis.z * inputMovementVector.y;

		if (Input.IsActionJustPressed("ui_cancel"))
		{
			if (Input.GetMouseMode() == Input.MouseMode.Visible)
			{
				Input.SetMouseMode(Input.MouseMode.Captured);
			}
			else
			{
				Input.SetMouseMode(Input.MouseMode.Visible);
			}
		}
	}

	private void ProcessMovement(float delta)
	{
		_direction.y = 0;
		_direction = _direction.Normalized();
		Vector3 hVelocity = velocity;
		hVelocity.y = 0;

		Vector3 target = _direction * MaxSpeed;
		float accel = _direction.Dot(hVelocity) > 0 ? Acceleration : Deacceleration;

		hVelocity = hVelocity.LinearInterpolate(target, accel * delta);
		velocity.x = hVelocity.x;
		velocity.z = hVelocity.z;
		velocity = MoveAndSlide(velocity, new Vector3(0, 1, 0), false, 4, Mathf.Deg2Rad(MaxSlopeAngle));
	}

	private void ProcessWarmth(float delta)
	{
		_warmth -= (velocity.Length() > 0.1 ? MovingWarmthLoss : IdleWarmthLoss) * delta;
		_warmth = Mathf.Clamp(_warmth, 0, MAX_WARMTH);
		_coldOverlay.Value = (float)(1.0 - (_warmth / MAX_WARMTH));

	}
	
}
