using Godot;
using System;

public class GranolaBar : Spatial
{
    [Export]
    public int WarmthToRestore = 100;

    public void _on_Area_body_entered(Node body)
    {
        GD.Print("Granola Touch");
        if (body is Player && body != null)
        {

            Player player = body as Player;
            GetCollectedBy(player);
        }
    }

    private void GetCollectedBy(Player player)
    {
        GD.Print("Granola Eat");
        player.Heal(WarmthToRestore);
        QueueFree();
    }
}
