using Godot;
using System;

public partial class striker_mundo : CharacterBody2D
{
	float velocidad = 100;
	Vector2 distancia = new Vector2(0, 0);

    private Area2D _area;

    public override void _Ready()
    {
        _area = GetNode<Area2D>("Area2D");

        _area.BodyEntered += OnBodyEntered;
    }
    public override void _Process(double delta)
    {
        var pj = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        if(distancia.X!=0){
            pj.Animation = "Walk";
        }
        else{
            pj.Animation = "Idle";
        }

        if(distancia.Length() >0){
            pj.Play();
        }
        
    }

    public override void _PhysicsProcess(double delta)
    {
        distancia.X = Convert.ToInt32(Input.IsActionPressed("ui_right")) - Convert.ToInt32(Input.IsActionPressed("ui_left"));

        distancia.X *= velocidad * (float) delta;

        distancia.Y = Convert.ToInt32(Input.IsActionPressed("ui_down")) - Convert.ToInt32(Input.IsActionPressed("ui_up"));
        
        distancia.Y *= velocidad * (float) delta;

        MoveAndCollide(distancia);
    }
    private void OnBodyEntered(Node body)
    {
        if (body is MovimientoEnemigo.Movimiento_Enemigo)
        {
            GetTree().ChangeSceneToFile("res://Mundo/combate.tscn");
        }
    }

    public override void _ExitTree()
    {
        if (_area != null)
        {
            _area.BodyEntered -= OnBodyEntered;
        }
    }
}
