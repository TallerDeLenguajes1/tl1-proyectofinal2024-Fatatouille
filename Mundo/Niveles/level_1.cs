using Godot;
using System;
using MyGame;
using System.Threading.Tasks;

public partial class level_1 : Node2D
{
    private string Nombre;
    private StaticBody2D Lluvia;

	public override async void _Ready()
    {
        Lluvia = GetNode<StaticBody2D>("Lluvia");
        Global global = (Global)GetNode("/root/Global");
        string filePath = "user://DatosPersonaje.json";

        global.Estado = "vivo";

        DatosPersonaje datosPersonaje = await DatosPersonaje.CargarDatosAsync(filePath);
        global.stage = 1;

        string clima = await ClimaAPI.GetClima("Federal Republic of Germany");

        ConfigurarNivelSegunClima(clima);

        if(datosPersonaje != null)
        {
            global.vida = datosPersonaje.VidaActual;
            global.Seleccionado = datosPersonaje.PersonajeSeleccionado;
            global.EnemigosEliminados = datosPersonaje.EnemigosEliminados;

            if (global.Seleccionado != -1)
            {
                string personajeRuta = global.PathPersonaje(global.Seleccionado);
                PackedScene personajeScene = (PackedScene)ResourceLoader.Load(personajeRuta);
                Nombre = global.NombrePersonaje(global.Seleccionado);

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

            Guardar();
            
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

    private async void Guardar(){
        Global global = (Global)GetNode("/root/Global");

        DatosPersonaje datosPersonaje = new DatosPersonaje(
            Nombre, 
            global.vida,
            global.EnemigosEliminados.Count,
            global.stage,
            global.Seleccionado,
            global.EnemigosEliminados,
            global.Estado
        );

        string filePath = "user://DatosPersonaje.json";
        await datosPersonaje.GuardarDatosAsync(filePath);
    }

    private void ConfigurarNivelSegunClima(string clima)
    {
        Global global = (Global)GetNode("/root/Global");
        global.Clima = clima.ToLower();

        if(clima.ToLower()!="clear")
        {
            Lluvia.Visible = true;
            GD.Print("El clima es lluvioso. Activando efectos de lluvia.");
        }else{
            Lluvia.Visible = false;
            GD.Print("El clima está despejado. Configurando un día soleado.");
        }
    }
}
