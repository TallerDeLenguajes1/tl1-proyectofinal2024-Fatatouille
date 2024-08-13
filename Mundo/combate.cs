using Godot;
using System;
using MyGame.GUI;
using MovimientoEnemigo;

public partial class Combate : TileMap
{
    private CharacterBody2D jugador;
    private CharacterBody2D enemigo;
    private Accion AccionControl;
    private Random random = new Random();
    private int vida_enemigo = 100;
    private int accion_enemigo;

    public override void _Ready()
    {
        Global global = (Global)GetNode("/root/Global");

        Movimiento_Enemigo enemigoActual = global.EnemigoActual;
        
        AccionControl = GetNode<Accion>("Accion");

        AccionControl.AtacarPressed += OnAtacarPressed;
        AccionControl.CurarPressed += OnCurarPressed;

        if (global.Seleccionado != -1)
        {
            
            string personajeRuta = global.PathCombate(global.Seleccionado);
            PackedScene personajeScene = (PackedScene)ResourceLoader.Load(personajeRuta);
            PackedScene enemigoScene = (PackedScene)ResourceLoader.Load(global.EnemigoSeleccionado);

            if (personajeScene != null && enemigoScene != null)
            {
                Node personajeInstance = personajeScene.Instantiate();
                AddChild(personajeInstance);
                Node enemigoInstance = enemigoScene.Instantiate();
                AddChild(enemigoInstance);

                if (personajeInstance is CharacterBody2D character)
                {
                    jugador = character;
                    character.Position = new Vector2(35, 168);
                    
                }
                if (enemigoInstance is CharacterBody2D enemy)
                {
                    enemigo = enemy;
                    enemy.Position = new Vector2(365, 175);
                }
                jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
                enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
            }
            else
            {
                GD.PrintErr("No se pudo cargar la escena del personaje.");
            }
        }
    }
    private void OnAtacarPressed()
    {
        Global global = (Global)GetNode("/root/Global");
        var timer = GetTree().CreateTimer(2);
        
        AccionControl.Visible = false;

        vida_enemigo -= random.Next(101);
        jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Atacar");
        enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("GetHit");
        timer.Timeout+= AccionEnemigo;
        GetTree().CreateTimer(5).Timeout += MostrarHUD;

        if (vida_enemigo <=0){
            enemigo.QueueFree();
            GetTree().ChangeSceneToFile($"res://Mundo/Niveles/level_{global.stage}.tscn");
        }
    }

    private void OnCurarPressed()
    {
        var timer = GetTree().CreateTimer(2);

        Global global = (Global)GetNode("/root/Global");
        global.vida += random.Next(51);
        if(global.vida >= 100){
            global.vida = 100;
        }
        jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Cura");
        enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
        AccionControl.Visible = false;

        timer.Timeout+= AccionEnemigo;
        GetTree().CreateTimer(5).Timeout += MostrarHUD;
    }

    private void AccionEnemigo(){
        accion_enemigo = random.Next(1, 3); //1: Ataca, 2: Cura

        if(accion_enemigo==1){
            Global global = (Global)GetNode("/root/Global");
            global.vida -= random.Next(51);
            enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Atacar");
            jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("GetHit");
            if(global.vida <= 0){
                jugador.QueueFree();
                GetTree().ChangeSceneToFile($"res://Mundo/Niveles/level_{global.stage}.tscn");
            }
        }else{
            vida_enemigo += random.Next(5, 51);
            enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Cura");
            jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
        }
    }

    private void MostrarHUD(){
        AccionControl.Visible = true;
        jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
        enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
    }

}