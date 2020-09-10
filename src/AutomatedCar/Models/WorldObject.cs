using ReactiveUI;

namespace AutomatedCar.Models {
    public abstract class WorldObject : ReactiveObject {

        public int ZIndex { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private int _x;
        private int _y;
        public int X {
            get => _x;
            set => this.RaiseAndSetIfChanged (ref _x, value);
        }
        public int Y {
            get => _y;
            set => this.RaiseAndSetIfChanged (ref _y, value);
        }

        public string Filename { get; set; }

        public WorldObject (int x, int y, string filename) {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.ZIndex = 1;
        }
    }
}