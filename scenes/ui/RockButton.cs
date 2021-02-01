using Godot;
using System;

public class RockButton : TextureButton
{
    [Signal]
    public delegate void RockSelected(int ID);

    [Export]
    public int ID;

    public override void _Ready()
    {
        Disabled = true;
        //ToggleMode = false;
        Visible = true;
    }

    public void _on_RockButton_toggled(bool buttonPressed)
    {
        GD.Print("toggle" + buttonPressed.ToString()); 
        if (buttonPressed)
        {
            EmitSignal(nameof(RockSelected), ID);
            Visible = false;
        }
    }

    public void _on_RockButton_button_down()
    {
        EmitSignal(nameof(RockSelected), ID);
        Visible = false;
    }

    public void Reset()
    {
        //ToggleMode = false;
        Visible = true;
    }
}
