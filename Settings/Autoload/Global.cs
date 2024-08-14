using Godot;
using System;
using MovimientoEnemigo;
using System.Collections.Generic;

public partial class Global : Node
{
    public int Seleccionado { get; set; } = -1; // -1 indica que ningún personaje está seleccionado.
    public int vida { get; set; } = 100; //Empieza full vidsa
    public int stage {get; set; }= 1;
    public string Nombre {get; set;}
    public static Global Instance {get; private set; }

    public int PersonajeCombate{ get; set; } = -1;
    public string EnemigoSeleccionado { get; set; }
    public Movimiento_Enemigo EnemigoActual { get; set; }

    public List<string> EnemigosEliminados { get; set; } = new List<string>();
    
    public string Estado {get; set;}

    public string Clima {get; set;}

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
    public string NombrePersonaje(int personaje)
    {
        switch (personaje)
        {
            case 0:
                return "King"; 
            case 1:
                return "Ronin"; 
            case 2:
                return "Striker"; 
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