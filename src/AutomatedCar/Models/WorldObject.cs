namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using System.Collections.ObjectModel;
    using System.Drawing;

    public class PropertyChangedEventArgs : System.EventArgs
    {
        public string PropertyName { get; set; }
    }

    public class WorldObject
    {
        public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs args);

        public event PropertyChangedEventHandler PropertyChangedEvent;

        private int x;
        private int y;

        private double rotation;

        public WorldObject(int x, int y, string filename, int zindex = 1, bool collideable = false, WorldObjectType worldObjectType = WorldObjectType.Other)
        {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.ZIndex = zindex;
            this.Collideable = collideable;
            this.WorldObjectType = worldObjectType;
        }

        public int ZIndex { get; set; }

        public double Rotation
        {
            get => this.rotation;
            //set => this.RaiseAndSetIfChanged(ref this.rotation, value % 360);
            set
            {
                this.rotation = value % 360;
                this.PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs() { PropertyName = nameof(this.Rotation) });
            }
        }

        public int X
        {
            get => this.x;
            //set => this.RaiseAndSetIfChanged(ref this.x, value);
            set
            {
                this.x = value;
                this.PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs() { PropertyName = nameof(this.X) });
            }
        }

        public int Y
        {
            get => this.y;
            //set => this.RaiseAndSetIfChanged(ref this.y, value);
            set
            {
                this.y = value;
                this.PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs() { PropertyName = nameof(this.Y) });
            }
        }

        public Point RotationPoint { get; set; }

        public string RenderTransformOrigin { get; set; }

        public ObservableCollection<PolylineGeometry> Geometries { get; set; } = new ();

        public ObservableCollection<PolylineGeometry> RawGeometries { get; set; } = new ();

        public string Filename { get; set; }

        public bool Collideable { get; set; }

        public WorldObjectType WorldObjectType { get; set; }
    }
}