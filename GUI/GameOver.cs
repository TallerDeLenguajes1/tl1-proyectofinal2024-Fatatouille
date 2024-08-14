using Godot;
using System;
using MyGame;

public partial class GameOver : CanvasLayer
{
	private Label EstadoLabel, PersonajeLabel, EnemigosLabel, StageLabel;

	private DatosPersonaje datosPersonaje;

	private Button Salir;
	public override async void _Ready()
	{
		Salir = GetNode<Button>("Salir");

		EstadoLabel = GetNode<Label>("Estado");
		PersonajeLabel = GetNode<Label>("Personaje");
		EnemigosLabel = GetNode<Label>("Enemigos");
		StageLabel = GetNode<Label>("Stage");

        string filePath = "user://DatosPersonaje.json";
        datosPersonaje = await DatosPersonaje.CargarDatosAsync(filePath);

        if (datosPersonaje != null)
        {
			if(datosPersonaje.Estado == "muerto"){
				EstadoLabel.Text = "Has muerto";
			}else{
				EstadoLabel.Text = "Ganaste!";
			}
            PersonajeLabel.Text = $"Personaje utilizado: {datosPersonaje.Personaje}";
            StageLabel.Text = $"Máximo nivel alcanzado: {datosPersonaje.Stage}";
            EnemigosLabel.Text = $"Enemigos asesinados: {datosPersonaje.VidaActual}";
        }
        else
        {
            GD.PrintErr("No se pudieron cargar los datos del personaje.");
        }

		Salir.Pressed += OnSalirPressed;
	}

	private void OnSalirPressed()
	{
		EliminarDatosGuardados();
		GetTree().ChangeSceneToFile("res://GUI/main.tscn");
	}

	private void EliminarDatosGuardados()
	{
		string filePath = "user://DatosPersonaje.json";
		string fullPath = ProjectSettings.GlobalizePath(filePath);

		if (System.IO.File.Exists(fullPath))
		{
			try
			{
				System.IO.File.Delete(fullPath);
				GD.Print("Datos de personaje eliminados.");
			}
			catch (Exception ex)
			{
				GD.PrintErr($"Error al eliminar los datos: {ex.Message}");
			}
		}
		else
		{
			GD.Print("No se encontraron datos de personaje para eliminar.");
		}
	}

}
