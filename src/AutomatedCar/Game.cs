using System;
using System.Linq;
using AutomatedCar.Models;
using Avalonia.Input;

namespace AutomatedCar {
    using Models;

    public class Game : GameBase {
        private readonly World world;

        public World World { get => world; }

        public Game (World world) {
            this.world = world;
        }

        private Random Random { get; } = new Random ();

        protected override void Tick () {
            if (Keyboard.IsKeyDown (Key.Up)) {
                world.ControlledCar.Y -= 5;
            } else if (Keyboard.IsKeyDown (Key.Down)) {
                world.ControlledCar.Y += 5;
            } else if (Keyboard.IsKeyDown (Key.Left)) {
                world.ControlledCar.X -= 5;
            } else if (Keyboard.IsKeyDown (Key.Right)) {
                world.ControlledCar.X += 5;
            }

        }
    }
}