using System;
namespace AutomatedCar.Models {
    /// <summary>This is a dummy object for demonstrating the codebase.</summary>
    public class Circle : WorldObject {
        public Circle (int x, int y, string filename, int radius) : base (x, y, filename) {
            Radius = radius;
        }
        public int Radius { get; set; }

        public double CalculateArea () {
            return Math.PI * Radius * Radius;
        }
    }
}