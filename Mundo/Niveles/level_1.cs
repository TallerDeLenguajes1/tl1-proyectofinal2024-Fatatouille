using Godot;
using System;

public partial class level_1 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        // Obtén la instancia del script global.
        Global global = (Global)GetNode("/root/Global");

        // Verifica que un personaje haya sido seleccionado.
        if (global.Seleccionado != -1)
        {
            // Obtén la ruta del archivo del personaje seleccionado.
            string personajeRuta = global.PathPersonaje(global.Seleccionado);
            PackedScene personajeScene = (PackedScene)ResourceLoader.Load(personajeRuta);

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
    }
}
