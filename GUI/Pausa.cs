using Godot;
using System;
using MyGame;
using Teclas;

public partial class Pausa : CanvasLayer
{
	private Label NameLabel, StageLabel, ClimaLabel, HpLabel;
	private DatosPersonaje datosPersonaje;

    private PanelContainer panel;

	private Button Continuar, Salir;

	private Button[] botones;
    private int botonSeleccionado = 0;

    public override void _Ready()
    {
        panel = GetNode<PanelContainer>("Player/PanelContainer");

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
        Global global = (Global)GetNode("/root/Global");

        if (datosPersonaje != null)
        {
            PackedScene packedScene = (PackedScene)ResourceLoader.Load($"res://Personajes/{datosPersonaje.Personaje.ToLower()}_combate.tscn");
            Node sceneInstance = packedScene.Instantiate();
            panel.AddChild(sceneInstance);
            sceneInstance.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
            sceneInstance.GetNode<Label>("Label").Visible = false;
            if (sceneInstance is CharacterBody2D character)
            {
                character.Position = new Vector2(15, 35);
            }

            NameLabel.Text = datosPersonaje.Personaje;
            StageLabel.Text = $"Stage: {datosPersonaje.Stage}";
            ClimaLabel.Text = $"Clima: {global.Clima}";
            HpLabel.Text = $"HP: {datosPersonaje.VidaActual}";
        }
        else
        {
            GD.PrintErr("No se pudieron cargar los datos del personaje.");
        }
    }

}
