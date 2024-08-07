using Godot;
using System;

public partial class Global : Node
{
    public int Seleccionado { get; set; } = -1; // -1 indica que ningún personaje está seleccionado.

    public override void _Ready()
    {
        GD.Print("Global script cargado.");
    }

    public string PathPersonaje(int personajeId)
    {
        // Devuelve la ruta del archivo del personaje basado en el ID.
        switch (personajeId)
        {
            case 0:
                return "res://Personajes/King_mundo.tscn"; 
            case 1:
                return "res://Personajes/ronin_mundo.tscn"; 
            case 2:
                return "res://Personajes/striker_mundo.tscn"; 
            default:
                GD.PrintErr("ID de personaje no válido.");
                return "";
        }
    }
}