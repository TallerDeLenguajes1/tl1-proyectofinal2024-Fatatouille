using Godot;
using System;
using MyGame.GUI;

public partial class Combate : TileMap
{
    private CharacterBody2D jugador;
    private CharacterBody2D enemigo;
    private Accion AccionControl;
    private Random random = new Random();
    public int vida_maxima= 100;
    public int vida= 100;
    private Node2D node2D;
    private AnimationPlayer animationPlayer;
    private ProgressBar progressBar;

    public override void _Ready()
    {
        AccionControl = GetNode<Accion>("Accion");

        AccionControl.AtacarPressed += OnAtacarPressed;
        AccionControl.CurarPressed += OnCurarPressed;

        Global global = (Global)GetNode("/root/Global");

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
                    node2D = enemigo.GetNode<Node2D>("Node2D");
                    animationPlayer = enemigo.GetNode<AnimationPlayer>("AnimationPlayer");
                    progressBar = enemigo.GetNode<ProgressBar>("ProgressBar");
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
        int damage = random.Next(0, 101);
        jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Atacar");
        enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("GetHit");
        CallDeferred(nameof(HacerDano), damage);
        AccionControl.Visible = false;
    }

    private void OnCurarPressed()
    {
        int maxVida = (int)jugador.Get("vida_maxima");
        int heal = random.Next(maxVida / 10, maxVida + 1);
        jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Cura");
        jugador.CallDeferred(nameof(Curacion), heal);
        AccionControl.Visible = false;
    }

    private void HacerDano(int damage)
    {
        enemigo.Set("vida", (int)enemigo.Get("vida") - damage);
        VerificarTurno();
    }

    private void Curacion(int heal)
    {
        jugador.Set("vida", Math.Min((int)jugador.Get("vida") + heal, (int)jugador.Get("vida_maxima")));
        VerificarTurno();
    }

    private void VerificarTurno()
    {
        if (!jugador.GetNode<AnimatedSprite2D>("AnimatedSprite2D").IsPlaying() &&
            !enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D").IsPlaying())
        {
            AccionControl.Visible = true;
        }
    }

}