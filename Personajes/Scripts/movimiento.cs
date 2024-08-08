using Godot;
using System;

namespace Movimiento
{
    public partial class Movimiento : CharacterBody2D
    {
        [Export]
        public float Velocidad { get; set; } = 100;

        private Vector2 _distancia = new Vector2(0, 0);
        private Area2D _area;

        public override void _Ready()
        {
            _area = GetNode<Area2D>("Area2D");

            _area.BodyEntered += OnBodyEntered;
        }

        public override void _Process(double delta)
        {
            var pj = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

            if (_distancia.X > 0)
            {
                pj.Animation = "Run_right";
                pj.FlipV = false;
                pj.FlipH = _distancia.X < 0;
            }
            else if (Input.IsActionPressed("ui_left"))
            {
                pj.Animation = "Run_left";
                pj.FlipV = false;
                pj.FlipH = _distancia.X > 0;
            }
            else if (Input.IsActionPressed("ui_up"))
            {
                pj.Animation = "Run_up";
                pj.FlipV = _distancia.Y > 0;
            }
            else if (Input.IsActionPressed("ui_down"))
            {
                pj.Animation = "Run_down";
                pj.FlipV = _distancia.Y < 0;
            }
            else
            {
                pj.Animation = "Idle";
            }

            if (_distancia.Length() > 0)
            {
                pj.Play();
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            _distancia.X = Convert.ToInt32(Input.IsActionPressed("ui_right")) - Convert.ToInt32(Input.IsActionPressed("ui_left"));
            _distancia.X *= Velocidad * (float)delta;

            _distancia.Y = Convert.ToInt32(Input.IsActionPressed("ui_down")) - Convert.ToInt32(Input.IsActionPressed("ui_up"));
            _distancia.Y *= Velocidad * (float)delta;

            MoveAndCollide(_distancia);
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
}
