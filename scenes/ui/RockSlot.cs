using Godot;
using System;

public class RockSlot : TextureRect
{
    [Export]
    public Texture EmptyTexture;
    [Export]
    public Texture BlankTexture;
    [Export]
    public Texture HeartTexture;
    [Export]
    public Texture MoonTexture;
    [Export]
    public Texture OctoTexture;
    [Export]
    public Texture SquareTexture;
    [Export]
    public Texture StarTexture;

    public override void _Ready()
    {
        this.Texture = EmptyTexture;
    }

    public void Fill(int ID)
    {
        switch (ID)
        {
            case 0:
                this.Texture = BlankTexture;
                break;
            case 1:
                this.Texture = HeartTexture;
                break;
            case 2:
                this.Texture = MoonTexture;
                break;
            case 3:
                this.Texture = OctoTexture;
                break;
            case 4:
                this.Texture = SquareTexture;
                break;
            case 5:
                this.Texture = StarTexture;
                break;
            default:
                this.Texture = EmptyTexture;
                break;
        }
    }

    public void Reset()
    {
        this.Texture = EmptyTexture;
    }
}
