using Godot;
using System;
using MovimientoEnemigo;
using System.Collections.Generic;

public partial class Global : Node
{
    public int Seleccionado { get; set; } = -1; // -1 indica que ningún personaje está seleccionado.
    public int vida { get; set; } = 100; //Empieza full vidsa
    public int nivel {get; set; } = 1;
    public int fuerza {get; set; } = 10;
    public int stage {get; set; }= 1;

    public static Global Instance {get; private set; }

    public int PersonajeCombate{ get; set; } = -1;
    public string EnemigoSeleccionado { get; set; }
    public Movimiento_Enemigo EnemigoActual { get; set; }

    public List<string> EnemigosEliminados { get; set; } = new List<string>();
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