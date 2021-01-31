using Godot;
using System;

public class Campfire : StaticBody
{
    public void _on_Area_body_entered(Node body)
    {
        if (body is Player && body != null)
        {
            GD.Print("Fire");
            Player p = body as Player;
            p.WarmUp();
        }
    }

    public void _on_Area_body_exited(Node body)
    {
        if (body is Player && body != null)
        {
            Player p = body as Player;
            p.StopWarmUp();
        }
    }
}
