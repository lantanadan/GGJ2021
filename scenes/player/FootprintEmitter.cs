using Godot;
using System;

public class FootprintEmitter : Spatial
{   
    [Export]
    public PackedScene FootprintScene = ResourceLoader.Load("res://scenes/visuals/Footprint.tscn") as PackedScene;
    [Export]
    public float SpeedThreshold = 0.1f;
    
    private AudioStreamPlayer3D _audio; // sound effect
    
    public override void _Ready()
    {
        _audio = GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");
    }

    public void _on_SoundTimer_timeout()
    {
        if (IsMoving())
        {
            _audio.Play();
        }
    }

    public void _on_VisualTimer_timeout()
    {
        if (IsMoving())
        {
            Footprint footprint = FootprintScene.Instance() as Footprint;
            footprint.GlobalTransform = GlobalTransform;
            GetParent().GetParent().AddChild(footprint);
        }
    }

    private bool IsMoving()
    {
        return GetParent<Player>().velocity.Length() > SpeedThreshold;
    }
}
