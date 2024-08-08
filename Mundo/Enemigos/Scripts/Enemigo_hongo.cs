using Godot;
using MovimientoEnemigo;

public partial class Enemigo_hongo : Movimiento_Enemigo
{
    public override void _Ready()
    {
        base._Ready();
        Connect("BodyEntered", new Callable(this, "OnBodyEntered"));
    }
}
