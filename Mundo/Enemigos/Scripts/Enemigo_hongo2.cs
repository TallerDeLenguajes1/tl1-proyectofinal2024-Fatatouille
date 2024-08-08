using Godot;
using MovimientoEnemigo;

public partial class Enemigo_hongo2 : Movimiento_Enemigo
{
    public override void _Ready()
    {
        base._Ready();
		Global global = (Global)GetNode("/root/Global");
        global.EnemigoSeleccionado = "res://Mundo/Enemigos/Hongo_combate.tscn";
    }
}