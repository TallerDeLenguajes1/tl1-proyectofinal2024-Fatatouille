using Godot;
using System;
using MyGame.GUI;
using System.Security.Cryptography.X509Certificates;

public partial class Combate : TileMap
{
    private CharacterBody2D jugador;
    private CharacterBody2D enemigo;
    private Accion AccionControl;
    private AnimationPlayer animationPlayer;
    private Random random = new Random();
    private int vida_enemigo = 100;
    private int accion_enemigo;

    public override void _Ready()
    {
        Global global = (Global)GetNode("/root/Global");

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
                    character.Position = new Vector2(25, 168);
                    
                }
                if (enemigoInstance is CharacterBody2D enemy)
                {
                    enemigo = enemy;
                    enemy.Position = new Vector2(365, 170);
                    animationPlayer = enemigo.GetNode<AnimationPlayer>("AnimationPlayer");
                }
            }
            else
            {
                GD.PrintErr("No se pudo cargar la escena del personaje.");
            }
        }
    }
    private void OnAtacarPressed()
    {
        var timer = GetTree().CreateTimer(2);
        
        AccionControl.Visible = false;

        vida_enemigo -= random.Next(101);
        jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Atacar");
        enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("GetHit");
        timer.Timeout+= AccionEnemigo;
    }

    private void OnCurarPressed()
    {
        jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Cura");
        AccionControl.Visible = false;
    }

    private void AccionEnemigo(){
        accion_enemigo = random.Next(1, 3); //1: Ataca, 2: Cura

        if(accion_enemigo==1){
            Global global = (Global)GetNode("/root/Global");
            global.vida -= random.Next(51);
            enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Atacar");
            jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("GetHit");
        }else{
            vida_enemigo += random.Next(5, 51);
            
        }
    }

}