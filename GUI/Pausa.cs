using Godot;
using System;
using MyGame;

public partial class Pausa : CanvasLayer
{
	private Label name, stage, clima, hp;
	public override void _PhysicsProcess(double delta)
	{
		if(Input.IsActionJustPressed("ui_cancel"))
		{
			GetTree().Paused = !GetTree().Paused;
			Visible = !Visible;
		}
	}
}
