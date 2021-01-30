using Godot;
using System;

public class Footprint : Spatial
{
    public override void _Ready()
    {
        AnimationPlayer p = GetNode<AnimationPlayer>("AnimationPlayer");
        p.Play("FadeOut");
    }

    public void Despawn()
    {
        QueueFree();
    }
}
