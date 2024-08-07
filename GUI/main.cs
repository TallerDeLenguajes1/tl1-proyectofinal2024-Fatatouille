using Godot;
using System;
using Teclas;

public partial class main : CanvasLayer
{
	private Button botonIniciar;
	private Button botonSalir;
	private Button[] botones;
    private int botonSeleccionado = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		botonIniciar = GetNode<Button>("Iniciar");
		botonSalir = GetNode<Button>("Salir");

		botones = new Button[] { botonIniciar, botonSalir };
        botones[botonSeleccionado].GrabFocus();

		botonIniciar.Pressed += OnIniciarPressed;
        botonSalir.Pressed += OnSalirPressed;
	}

	public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_down"))
        {
            Navegacion.MoverFoco(botones, ref botonSeleccionado, 1);
        }

        if (@event.IsActionPressed("ui_up"))
        {
            Navegacion.MoverFoco(botones, ref botonSeleccionado, -1);
        }

        if (@event.IsActionPressed("ui_accept"))
        {
            Navegacion.ActivarBoton(botones, botonSeleccionado);
        }

        if (@event.IsActionPressed("ui_cancel"))
        {
            botonSalir.EmitSignal("pressed");
        }
    }

	private void OnIniciarPressed()
    {
        GetTree().ChangeSceneToFile("res://GUI/selector_personaje.tscn");
    }

    private void OnSalirPressed()
    {
        GetTree().Quit();
    }
}
