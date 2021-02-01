using Godot;
using System;

public class MagicRockPedestal : Spatial
{
    public void _on_Area_body_entered(Node body)
    {
        if (body is Player && body != null)
        {
            Player player = body as Player;
            player.OpenRockMenu();
        }
    }

    public void _on_Area_body_exited(Node body)
    {
        if (body is Player && body != null)
        {
            Player player = body as Player;
            player.CloseRockMenu();
        }
    }
}

