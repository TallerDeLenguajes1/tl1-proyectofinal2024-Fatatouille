using Godot;
using System;

public partial class combate : TileMap
{
	public override void _Ready()
    {
        Global global = (Global)GetNode("/root/Global");

        if (global.Seleccionado != -1)
        {
            
            string personajeRuta = global.PathCombate(global.Seleccionado);
            PackedScene personajeScene = (PackedScene)ResourceLoader.Load(personajeRuta);

            if (personajeScene != null)
            {
                Node personajeInstance = personajeScene.Instantiate();
                AddChild(personajeInstance);

				if (personajeInstance is CharacterBody2D character)
                {
                    character.Position = new Vector2(20, 168);
                }
            }
            else
            {
                GD.PrintErr("No se pudo cargar la escena del personaje.");
            }
        }
    }
}
