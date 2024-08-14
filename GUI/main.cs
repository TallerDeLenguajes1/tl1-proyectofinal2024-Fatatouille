using Godot;
using System;
using Teclas;
using MyGame;
using System.IO;
using System.Text.Json;

public partial class main : CanvasLayer
{
	private Button botonIniciar;
	private Button botonSalir;
	private Button[] botones;
    private int botonSeleccionado = 0;

	private string filePath = "user://DatosPersonaje.json";
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

	private async void OnIniciarPressed()
    {
        if (File.Exists(ProjectSettings.GlobalizePath(filePath)))
        {
            GD.Print("Datos guardados encontrados, cargando el juego guardado...");

            string json = await File.ReadAllTextAsync(ProjectSettings.GlobalizePath(filePath));
            var datosPersonaje = JsonSerializer.Deserialize<DatosPersonaje>(json);

            if (datosPersonaje != null)
            {
                string nivelPath = $"res://Mundo/Niveles/level_{datosPersonaje.Stage}.tscn";
                GD.Print($"Cargando el nivel: {nivelPath}");
                GetTree().ChangeSceneToFile(nivelPath);
            }
            else
            {
                GD.PrintErr("Error al deserializar los datos guardados.");
                GetTree().ChangeSceneToFile("res://GUI/selector_personaje.tscn");
            }
        }
        else
        {
            GD.Print("No se encontraron datos guardados, redirigiendo a la selección de personaje...");
            GetTree().ChangeSceneToFile("res://GUI/selector_personaje.tscn");
        }
    }

    private void OnSalirPressed()
    {
        GetTree().Quit();
    }
}
