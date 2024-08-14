using Godot;
using System;
using MyGame;

public partial class level_1 : Node2D
{
    private string Nombre;

	public override async void _Ready()
    {
        Global global = (Global)GetNode("/root/Global");
        string filePath = "user://DatosPersonaje.json";

        global.Estado = "vivo";

        DatosPersonaje datosPersonaje = await DatosPersonaje.CargarDatosAsync(filePath);
        global.stage = 1;

        if(datosPersonaje != null)
        {
            global.vida = datosPersonaje.VidaActual;
            global.Seleccionado = datosPersonaje.PersonajeSeleccionado;
            global.EnemigosEliminados = datosPersonaje.EnemigosEliminados;

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
        
    }

    private async void Guardar(){
        Global global = (Global)GetNode("/root/Global");

        DatosPersonaje datosPersonaje = new DatosPersonaje(
            Nombre, 
            global.vida,
            global.EnemigosEliminados.Count,
            global.stage,
            global.Seleccionado,
            global.EnemigosEliminados,
            global.Estado
        );

        string filePath = "user://DatosPersonaje.json";
        await datosPersonaje.GuardarDatosAsync(filePath);
    }
}
