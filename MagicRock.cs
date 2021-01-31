using Godot;
using System;

public class MagicRock : Spatial
{
    [Export]
    public int key; // 0="blank", 1="heart", 2="moon", 3="octo", 4="square", 5="star"

    public void _on_Area_body_entered(Node body)
    {
        if (body is Player && body != null)
        {

            Player player = body as Player;
            GetCollectedBy(player);
        }
    }

    private void GetCollectedBy(Player player)
    {
        player.CollectRock(key);
        QueueFree();
    }
}
