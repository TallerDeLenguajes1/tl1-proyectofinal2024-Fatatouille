using Godot;
using System;
using Teclas;
using MyGame;

public partial class selector_personaje : CanvasLayer
{

	private Button SeleccionarKing;
	private Button SeleccionarRonin;
	private Button SeleccionarStriker;

	private string Nombre;

	private Button volver;

	private Button[] botones;
    private int botonSeleccionado = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		SeleccionarKing = GetNode<Button>("Panel3/SeleccionarKing");
		SeleccionarRonin = GetNode<Button>("Panel4/SeleccionarRonin");
		SeleccionarStriker = GetNode<Button>("Panel5/SeleccionarStriker");

		botones = new Button[] { SeleccionarKing, SeleccionarRonin, SeleccionarStriker };

        botones[botonSeleccionado].GrabFocus();

		SeleccionarKing.Pressed += OnKingPressed;
		SeleccionarRonin.Pressed += OnRoninPressed;
		SeleccionarStriker.Pressed += OnStrikerPressed;

		volver = GetNode<Button>("Atras");
		volver.Pressed += OnAtrasPressed;
	}

	public override void _Input(InputEvent @event)
	{

		if (@event.IsActionPressed("ui_right"))
		{
			Navegacion.MoverFoco(botones, ref botonSeleccionado, 1);
		}
		if (@event.IsActionPressed("ui_left"))
		{
			Navegacion.MoverFoco(botones, ref botonSeleccionado, -1);
		}
		if (@event.IsActionPressed("ui_down"))
		{
			botones[botonSeleccionado].ReleaseFocus();
            volver.GrabFocus();
		}
		if (@event.IsActionPressed("ui_up"))
		{
			volver.ReleaseFocus();
			botonSeleccionado = 0;
			botones[botonSeleccionado].GrabFocus();
		}

		if (@event.IsActionPressed("ui_cancel"))
        {
            volver.EmitSignal("pressed");
        }

		if (@event.IsActionPressed("ui_accept"))
        {
            if (volver.HasFocus())
            {
                OnAtrasPressed();
            }
            else
            {
                Navegacion.ActivarBoton(botones, botonSeleccionado);
            }
        }
	}

	private void OnAtrasPressed()
    {
        GetTree().ChangeSceneToFile("res://GUI/main.tscn");
    }

	private void OnKingPressed(){
		SeleccionarPersonaje(0);
	}
	private void OnRoninPressed(){
		SeleccionarPersonaje(1);
	}
	private void OnStrikerPressed(){
		SeleccionarPersonaje(2);
	}

	private void SeleccionarPersonaje(int personajeId)
	{
		Global global = (Global)GetNode("/root/Global");
		
		global.Seleccionado = personajeId;
		global.PersonajeCombate = personajeId;

		Nombre = global.NombrePersonaje(personajeId);

		GD.Print($"Personaje {personajeId} seleccionado.");

		Guardar();

		GetTree().ChangeSceneToFile("res://Mundo/Niveles/level_1.tscn");
	}

	private async void Guardar(){
        Global global = (Global)GetNode("/root/Global");

        DatosPersonaje datosPersonaje = new DatosPersonaje(
            Nombre, 
            100,
            0,
            1,
			global.Seleccionado,
			global.EnemigosEliminados,
			"vivo"
        );

        string filePath = "user://DatosPersonaje.json";
        await datosPersonaje.GuardarDatosAsync(filePath);
    }

}
