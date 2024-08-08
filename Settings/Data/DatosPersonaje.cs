using Godot;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyGame
{
    public class DatosPersonaje
    {
        public string Personaje { get; set; }
        public int Nivel { get; set; }
        public int VidaActual { get; set; }
        public int VidaMaxima { get; set; }
        public int CantidadPociones { get; set; }
        public string NivelActual { get; set; }

        public DatosPersonaje(string personaje, int nivel, int vidaActual, int vidaMaxima, int cantidadPociones, string nivelActual)
        {
            Personaje = personaje;
            Nivel = nivel;
            VidaActual = vidaActual;
            VidaMaxima = vidaMaxima;
            CantidadPociones = cantidadPociones;
            NivelActual = nivelActual;
        }

        public void GuardarDatos(string filePath)
        {
            try
            {
                // Serializar el objeto a JSON usando System.Text.Json
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });

                // Guardar el JSON en un archivo
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                GD.PrintErr($"Error al guardar los datos: {ex.Message}");
            }
        }

        public static DatosPersonaje CargarDatos(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    GD.PrintErr("Archivo de datos no encontrado.");
                    return null;
                }

                // Leer el JSON desde el archivo
                string json = File.ReadAllText(filePath);

                // Deserializar el JSON a un objeto DatosPersonaje
                return JsonSerializer.Deserialize<DatosPersonaje>(json);
            }
            catch (Exception ex)
            {
                GD.PrintErr($"Error al cargar los datos: {ex.Message}");
                return null;
            }
        }
    }
}
