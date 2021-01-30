using Godot;
using System;

public class Tree : StaticBody
{
    public override void _Ready()
    {
        Random r = new Random();
        int angle = r.Next(0, 360);
        RotationDegrees = new Vector3(RotationDegrees.x, angle, RotationDegrees.z);
    }
}
