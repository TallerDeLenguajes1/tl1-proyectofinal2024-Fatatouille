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

	private void OnAtrasPressed()
    {
        GetTree().ChangeSceneToFile("res://GUI/main.tscn");
    }

}
