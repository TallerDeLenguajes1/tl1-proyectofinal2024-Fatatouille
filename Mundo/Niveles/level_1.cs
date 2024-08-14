using Godot;
using System;
using MyGame;
using System.Runtime.CompilerServices;

public partial class level_1 : Node2D
{
    private string Nombre;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Global global = (Global)GetNode("/root/Global");
        global.stage = 1;

        if (global.Seleccionado != -1)
        {
            string personajeRuta = global.PathPersonaje(global.Seleccionado);
            PackedScene personajeScene = (PackedScene)ResourceLoader.Load(personajeRuta);
            Nombre = global.NombrePersonaje(global.Seleccionado);

            if (personajeScene != null)
            {
                Node personajeInstance = personajeScene.Instantiate();
                AddChild(personajeInstance);

				if (personajeInstance is CharacterBody2D character)
                {
                    character.Position = new Vector2(15, 100);
                }
            }
            else
            {
                GD.PrintErr("No se pudo cargar la escena del personaje.");
            }
        }

        Guardar();
        
        foreach (string enemigoEliminado in global.EnemigosEliminados)
        {
            Node enemigo = GetNodeOrNull(enemigoEliminado);
            if (enemigo != null)
            {
                enemigo.QueueFree();
            }
        }
    }

    private async void Guardar(){
        Global global = (Global)GetNode("/root/Global");

        DatosPersonaje datosPersonaje = new DatosPersonaje(
            Nombre, 
            global.vida,
            global.EnemigosEliminados.Count,
            global.stage
        );

        string filePath = "res://Settings/Data/DatosPersonaje.json";
        await datosPersonaje.GuardarDatosAsync(filePath);
    }
}
