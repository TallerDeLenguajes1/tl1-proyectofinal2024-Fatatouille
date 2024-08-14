using Godot;
using System;
using MovimientoEnemigo;

public partial class Slimev_combate : Movimiento_Enemigo
{
    public override void _Ready()
    {
        base._Ready();
        NombreEnemigo = "Slimev";
    }
}