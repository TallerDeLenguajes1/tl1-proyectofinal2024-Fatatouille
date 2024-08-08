using Godot;
using System;

public partial class Global : Node
{
    public int Seleccionado { get; set; } = -1; // -1 indica que ningún personaje está seleccionado.

    public static Global Instance {get; private set; }

    public int PersonajeCombate{ get; set; } = -1;
    public string EnemigoSeleccionado { get; set; }
    public override void _Ready()
    {
        Instance = this;
    }

    public string PathPersonaje(int personajeId)
    {
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
    public string PathCombate(int personajeId)
    {
        switch (personajeId)
        {
            case 0:
                return "res://Personajes/king_combate.tscn"; 
            case 1:
                return "res://Personajes/ronin_combate.tscn"; 
            case 2:
                return "res://Personajes/striker_combate.tscn"; 
            default:
                GD.PrintErr("ID de personaje no válido.");
                return "";
        }
    }
}