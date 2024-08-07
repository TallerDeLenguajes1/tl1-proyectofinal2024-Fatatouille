using Godot;
using System;

namespace Teclas
{
    public static class Navegacion
    {
        // Mueve el foco a otro botón en la dirección especificada.
        public static void MoverFoco(Button[] botones, ref int botonSeleccionado, int direccion)
        {
            // Desactiva el foco del botón actual.
            botones[botonSeleccionado].ReleaseFocus();

            // Calcula el nuevo índice de botón seleccionado.
            botonSeleccionado = (botonSeleccionado + direccion + botones.Length) % botones.Length;

            // Establece el foco en el nuevo botón.
            botones[botonSeleccionado].GrabFocus();
        }

        // Emite la señal "pressed" del botón actualmente seleccionado.
        public static void ActivarBoton(Button[] botones, int botonSeleccionado)
        {
            botones[botonSeleccionado].EmitSignal("pressed");
        }
    }
}