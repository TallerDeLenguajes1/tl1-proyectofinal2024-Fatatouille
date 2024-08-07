using Godot;
using System;

public partial class selector_personaje : CanvasLayer
{
	private Button volver;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		volver = GetNode<Button>("Atras");
		volver.Pressed += OnAtrasPressed;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel"))
        {
            volver.EmitSignal("pressed");
        }
	}

	private void OnAtrasPressed()
    {
        GetTree().ChangeSceneToFile("res://GUI/main.tscn");
    }

}
