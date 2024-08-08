using Godot;

namespace Data{
    public partial class Data : CharacterBody2D
    {
        public int Vida { get; set;} = 100;
        public override void _Ready()
        {
            Vida = 100;
        }
        
    }
}