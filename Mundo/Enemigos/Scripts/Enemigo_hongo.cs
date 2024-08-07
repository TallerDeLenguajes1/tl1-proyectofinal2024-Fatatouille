using Godot;
using System;
using System.Collections.Generic;

public partial class Enemigo_hongo : CharacterBody2D
{
    [Export]
    public float Velocidad = 50f;

    private List<Marker2D> _puntosDePatrullaje = new List<Marker2D>();
    private int _puntoActual = 0;
    private Vector2 _posicionObjetivo;

    public override void _Ready()
    {
        
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
		
        MoveAndCollide(movimiento);
    }
}