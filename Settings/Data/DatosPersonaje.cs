using Godot;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyGame
{
    public class DatosPersonaje
    {
        public string Personaje { get; set; }
        public int VidaActual { get; set; } = 100;
        public int CantidadEnemigosEliminados { get; set; } = 0;

        public DatosPersonaje() { }

        public DatosPersonaje(string personaje, int vidaActual, int cantEnemigos)
        {
            Personaje = personaje;
            VidaActual = vidaActual;
            CantidadEnemigosEliminados = cantEnemigos;
        }

        public async Task GuardarDatosAsync(string filePath)
        {
            try
            {
                string fullPath = ProjectSettings.GlobalizePath(filePath);
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(fullPath, json);
            }
            catch (Exception ex)
            {
                GD.PrintErr($"Error al guardar los datos: {ex.Message}");
            }
        }

        public static async Task<DatosPersonaje> CargarDatosAsync(string filePath)
        {
            try
            {
                string fullPath = ProjectSettings.GlobalizePath(filePath);
                if (!File.Exists(fullPath))
                {
                    GD.PrintErr("Archivo de datos no encontrado.");
                    return null;
                }

                string json = await File.ReadAllTextAsync(fullPath);
                var datos = JsonSerializer.Deserialize<DatosPersonaje>(json);
                return datos.EsDatoValido() ? datos : null;
            }
            catch (Exception ex)
            {
                GD.PrintErr($"Error al cargar los datos: {ex.Message}");
                return null;
            }
        }

        public bool EsDatoValido()
        {
            return !string.IsNullOrEmpty(Personaje) && VidaActual >= 0 && CantidadEnemigosEliminados >= 0;
        }
    }
}