using System.Drawing;
using ReactiveUI;
using System.Drawing.Drawing2D;

namespace AutomatedCar.Models
{
    public class WorldObject : ReactiveObject
    {
        private int x;
        private int y;

        private double rotation;

        public WorldObject(int x, int y, string filename, int zindex = 1)
        {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.ZIndex = zindex;
        }

        public int ZIndex { get; set; }

        public double Rotation
        {
            get => this.rotation;
            set => this.RaiseAndSetIfChanged(ref this.rotation, value % 360);
        }

        public int X
        {
            get => this.x;
            set => this.RaiseAndSetIfChanged(ref this.x, value);
        }

        public int Y
        {
            get => this.y;
            set => this.RaiseAndSetIfChanged(ref this.y, value);
        }

        public Point RotationPoint { get; set; }

        public string RenderTransformOrigin { get; set; }

        public GraphicsPath Geometry { get; set; }

        public string Filename { get; set; }
    }
}