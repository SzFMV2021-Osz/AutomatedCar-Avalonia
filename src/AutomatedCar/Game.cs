namespace AutomatedCar
{
    using System;
    using AutomatedCar.Models;
    using Avalonia.Input;

    public class Game : GameBase
    {
        private readonly World world;

        public Game(World world)
        {
            this.world = world;
        }

        public World World { get => this.world; }

        private Random Random { get; } = new Random();

        protected override void Tick()
        {
            if (Keyboard.IsKeyDown(Key.Up))
            {
                this.world.ControlledCar.Y -= 5;
            }

            if (Keyboard.IsKeyDown(Key.Down))
            {
                this.world.ControlledCar.Y += 5;
            }

            if (Keyboard.IsKeyDown(Key.Left))
            {
                this.world.ControlledCar.X -= 5;
            }

            if (Keyboard.IsKeyDown(Key.Right))
            {
                this.world.ControlledCar.X += 5;
            }

            if (Keyboard.IsKeyDown(Key.PageUp))
            {
                this.world.ControlledCar.Rotation += 5;
            }

            if (Keyboard.IsKeyDown(Key.PageDown))
            {
                this.world.ControlledCar.Rotation -= 5;
            }
        }
    }
}