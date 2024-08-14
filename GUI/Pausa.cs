using Godot;
using System;
using MyGame;
using Teclas;

public partial class Pausa : CanvasLayer
{
	private Label NameLabel, StageLabel, ClimaLabel, HpLabel;
	private DatosPersonaje datosPersonaje;

	private Button Continuar, Salir;

	private Button[] botones;
    private int botonSeleccionado = 0;

    public override void _Ready()
    {
        NameLabel = GetNode<Label>("Player/Nombre");
        StageLabel = GetNode<Label>("Player/Stage");
        ClimaLabel = GetNode<Label>("Player/Clima");
        HpLabel = GetNode<Label>("Player/Vida");

		Continuar = GetNode<Button>("Control/VBoxContainer/Continuar");
		Salir = GetNode<Button>("Control/VBoxContainer/Salir");

		botones = new Button[] { Continuar, Salir };

		botones[botonSeleccionado].GrabFocus();

		Continuar.Pressed += Despausar;
		Salir.Pressed += OnSalirPressed;
    }

    public override void _PhysicsProcess(double delta)
	{
		if(Input.IsActionJustPressed("ui_cancel"))
		{
			Despausar();
		}
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
    }

	private void Despausar()
	{
		GetTree().Paused = !GetTree().Paused;
		Visible = !Visible;
		CargarDatos();
	}

	private void OnSalirPressed()
    {
        GetTree().Quit();
    }

	private async void CargarDatos()
    {
        string filePath = "user://DatosPersonaje.json";
        datosPersonaje = await DatosPersonaje.CargarDatosAsync(filePath);

        if (datosPersonaje != null)
        {
            NameLabel.Text = datosPersonaje.Personaje;
            StageLabel.Text = $"Stage: {datosPersonaje.Stage}";
            ClimaLabel.Text = "Clima: Soleado";
            HpLabel.Text = $"HP: {datosPersonaje.VidaActual}";
        }
        else
        {
            GD.PrintErr("No se pudieron cargar los datos del personaje.");
        }
    }

}
