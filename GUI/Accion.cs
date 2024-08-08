using Godot;
using System;
using Teclas;

namespace MyGame.GUI
{
    public partial class Accion : Control
    {	
        private Button _botonAtacar;
        private Button _botonCurar;
        public event Action AtacarPressed;
        public event Action CurarPressed;
		private Button[] botones;
    	private int botonSeleccionado = 0;

        public override void _Ready()
        {
            _botonAtacar = GetNode<Button>("VBoxContainer/Atacar");
            _botonCurar = GetNode<Button>("VBoxContainer/Curar");

			botones = new Button[] { _botonAtacar, _botonCurar };

        	botones[botonSeleccionado].GrabFocus();

            _botonAtacar.Pressed += OnAtacarPressed;
            _botonCurar.Pressed += OnCurarPressed;
        }

		public override void _Input(InputEvent @event)
		{
			if (@event.IsActionPressed("ui_up"))
			{
				Navegacion.MoverFoco(botones, ref botonSeleccionado, 1);
			}
			if (@event.IsActionPressed("ui_down"))
			{
				Navegacion.MoverFoco(botones, ref botonSeleccionado, -1);
			}
			if (@event.IsActionPressed("ui_accept"))
			{
				Navegacion.ActivarBoton(botones, botonSeleccionado);
			}
		}

        private void OnAtacarPressed()
        {
            AtacarPressed?.Invoke();
        }

        private void OnCurarPressed()
        {
            CurarPressed?.Invoke();
        }
    }
}