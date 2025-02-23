using Godot;
using System;
using System.Collections.Generic;

namespace MovimientoEnemigo
{
    public partial class Movimiento_Enemigo : CharacterBody2D
    {
        [Export]
        public float Velocidad = 50f;

        private List<Marker2D> _puntosDePatrullaje = new List<Marker2D>();
        private int _puntoActual = 0;
        private Vector2 _posicionObjetivo;
        private AnimatedSprite2D _sprite;
        private Area2D Area;

        public string NombreEnemigo {get; set;}

        public override void _Ready()
        {
            string currentScene = GetTree().CurrentScene.SceneFilePath;

            _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

            if(currentScene != "res://Mundo/Combate.tscn"){
                Area = GetNode<Area2D>("Area2D");
                Area.BodyEntered += OnBodyEntered;
            }

            foreach (Node child in GetChildren())
            {
                if (child is Marker2D marker)
                {
                    _puntosDePatrullaje.Add(marker);
                }
            }

            if (_puntosDePatrullaje.Count > 0)
            {
                _posicionObjetivo = _puntosDePatrullaje[_puntoActual].GlobalPosition;
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            if (_puntosDePatrullaje.Count == 0) return;

            Vector2 posicionActual = GlobalPosition;
            if (posicionActual.DistanceTo(_posicionObjetivo) < 5)
            {
                _puntoActual = (_puntoActual + 1) % _puntosDePatrullaje.Count;
                _posicionObjetivo = _puntosDePatrullaje[_puntoActual].GlobalPosition;
            }

            Vector2 direccion = (_posicionObjetivo - posicionActual).Normalized();
            Vector2 movimiento = direccion * Velocidad * (float)delta;

            if (Math.Abs(direccion.X) > Math.Abs(direccion.Y))
            {
                if (direccion.X > 0)
                {
                    _sprite.Play("Right");
                }
                else
                {
                    _sprite.Play("Left");
                }
            }
            else
            {
                if (direccion.Y > 0)
                {
                    _sprite.Play("Down");
                }
                else
                {
                    _sprite.Play("Up");
                }
            }

            MoveAndCollide(movimiento);
        }

        private void OnBodyEntered(Node body)
        {
            
            if (body is Movimiento.Movimiento)
            {
                Global global = GetNode<Global>("/root/Global");

                global.EnemigosEliminados.Add(this.Name);
                
                SetProcess(false);
                SetPhysicsProcess(false);
                GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);

                global.EnemigoSeleccionado = $"res://Mundo/Enemigos/{NombreEnemigo}_combate.tscn";

                CallDeferred("CambiarNivel");
            }
        }

        public override void _ExitTree()
        {
            if (Area != null)
            {
                Area.BodyEntered -= OnBodyEntered;
            }
        }

        private void CambiarNivel(){
            GetTree().ChangeSceneToFile("res://Mundo/Combate.tscn");
        }
        
    }
}