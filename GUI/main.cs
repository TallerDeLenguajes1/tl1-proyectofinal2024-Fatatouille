using Godot;
using System;

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
        // Detecta la navegación hacia abajo.
        if (@event.IsActionPressed("ui_down"))
        {
            MoverFoco(1);
        }

        // Detecta la navegación hacia arriba.
        if (@event.IsActionPressed("ui_up"))
        {
            MoverFoco(-1);
        }

        // Detecta la acción de aceptar.
        if (@event.IsActionPressed("ui_accept"))
        {
            botones[botonSeleccionado].EmitSignal("pressed");
        }
    }

    private void MoverFoco(int direccion)
    {
        // Desactiva el foco del botón actual.
        botones[botonSeleccionado].ReleaseFocus();

        // Calcula el nuevo índice de botón seleccionado.
        botonSeleccionado = (botonSeleccionado + direccion + botones.Length) % botones.Length;

        // Establece el foco en el nuevo botón.
        botones[botonSeleccionado].GrabFocus();
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
