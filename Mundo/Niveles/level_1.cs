using Godot;
using System;

public partial class level_1 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Global global = (Global)GetNode("/root/Global");
        global.stage = 1;

        if (global.Seleccionado != -1)
        {
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
