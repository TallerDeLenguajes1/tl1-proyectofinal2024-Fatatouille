using Godot;
using System;

public partial class Puerta : CharacterBody2D
{
	private AnimatedSprite2D SpritePuerta;
	private Area2D PuertaNivel;
	public override void _Ready()
	{
		SpritePuerta = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		PuertaNivel = GetNode<Area2D>("Area2D");

		SpritePuerta.Play("Cerrada");

		PuertaNivel.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node body)
	{
        if (body is Movimiento.Movimiento)
		{
			SetProcess(false);
			SetPhysicsProcess(false);
			GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
			SpritePuerta.Play("Abierta");
			PuertaNivel.QueueFree();
			GetTree().CreateTimer(1).Timeout += CambiarEscena;
		}
	}
	public override void _ExitTree()
        {
            if (PuertaNivel != null)
            {
                PuertaNivel.BodyEntered -= OnBodyEntered;
            }
        }

	private void CambiarEscena()
	{
		GetTree().ChangeSceneToFile("res://GUI/GameOver.tscn");
	}
}
