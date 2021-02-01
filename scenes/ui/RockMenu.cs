using Godot;
using System;

public class RockMenu : Control
{
    [Export]
    public int[] CorrectOrder = { 0, 1, 2, 3, 4, 5 };

    private Godot.Collections.Array<int> _current_order = new Godot.Collections.Array<int>();
    private Godot.Collections.Array<RockButton> _rock_btns = new Godot.Collections.Array<RockButton>();
    private Godot.Collections.Array<RockSlot> _rock_slots = new Godot.Collections.Array<RockSlot>();

    public override void _Ready()
    {
        for (int i = 0; i < 5; i ++)
        {
            GD.Print(i.ToString());
            GD.Print(GetNode<RockButton>("CenterContainer/ColorRect/VBoxContainer/HBoxContainer/Rock" + i.ToString()).ToString());
            RockButton btn = GetNode<RockButton>("CenterContainer/ColorRect/VBoxContainer/HBoxContainer/Rock" + i.ToString()) as RockButton;
            _rock_btns.Add(btn);
            RockSlot slot = GetNode<RockSlot>("CenterContainer/ColorRect/VBoxContainer/ColorRect/HBoxContainer2/RockSlot" + i.ToString()) as RockSlot;
            _rock_slots.Add(slot);
            
            _rock_btns[i].Connect("RockSelected", this, nameof(MoveRock));
        }
        Close();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
		{
			//Close();
		}
    }

    public void Open()
    {
        Visible = true;
        Input.SetMouseMode(Input.MouseMode.Visible);
    }

    public void Close()
    {
        Reset();
        Visible = false;
        Input.SetMouseMode(Input.MouseMode.Captured);
    }

    public void Reset()
    {
        foreach (RockButton btn in _rock_btns)
        {
            if (btn == null) continue;
            btn.Reset();
        }
        foreach (RockSlot slot in _rock_slots)
        {
            if (slot == null) continue;
            slot.Reset();
        }
        _current_order.Clear();
    }

    public void CollectRock(int ID)
    {
        _rock_btns[ID].Disabled = false;
    }

    private void MoveRock(int ID)
    {
        if (!_current_order.Contains(ID))
        {
            _current_order.Add(ID);
            _rock_slots[ID].Fill(ID);
        }
    }

    private bool IsOrderCorrect()
    {
        if (_current_order.Count != 6) return false;
        for (int i = 0; i < 5; i ++)
        {
            if (_current_order[i] != CorrectOrder[i]) return false;
        }
        return true;
    }
}
