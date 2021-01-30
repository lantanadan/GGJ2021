using Godot;
using System;

public class ColdOverlay : Control
{
    [Export]
    public Color MinColor;
    [Export]
    public Color MaxColor;
    [Export]
    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            UpdateOverlay(value);
        }
    }
    [Export]
    public float TransitionTime = 0.0f;

    private Tween _tween;
    private float _value = 0.0f;

    public override void _Ready()
    {
        _tween = GetNode<Tween>("ColdOpacity");
        UpdateOverlay(Value);
    }

    private void UpdateOverlay(float newValue)
    {
        GD.Print(newValue);
        _tween.InterpolateProperty(this, "modulate", RatioColor(_value), RatioColor(newValue),
            TransitionTime, Tween.TransitionType.Sine);
        _tween.Start();
        _value = newValue;
    }

    private Color RatioColor(float ratio)
    {
        return MinColor * (1 - ratio) + MaxColor * ratio;
    }
}
