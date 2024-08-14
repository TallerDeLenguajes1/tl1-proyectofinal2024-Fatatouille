using Godot;
using MovimientoEnemigo;

public partial class Enemigo_slimev : Movimiento_Enemigo
{
    public override void _Ready()
    {
        base._Ready();
        NombreEnemigo = "Slimev";
    }
}