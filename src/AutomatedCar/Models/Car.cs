namespace AutomatedCar.Models {
    public class Car : WorldObject {

        public Car (int x, int y, string filename) : base (x, y, filename) { }

        /// <summary>Speed in px/s.</summary>
        public int Speed { get; set; }
    }
}