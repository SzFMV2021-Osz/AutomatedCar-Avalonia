namespace AutomatedCar.Models
{
    using System.Collections.ObjectModel;
    using System.Drawing;
    using Avalonia.Media;
    using ReactiveUI;
    using System.Collections.Generic;

    public class WorldObject : ReactiveObject
    {
        private int x;
        private int y;

        private double rotation;

        public WorldObject(int x, int y, string filename)
        {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.WorldObjectType = this.DetermineType(filename);
            this.ZIndex = this.DetermineZIndex(filename);
            this.Collideable = this.DetermineCollidablity(filename);
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

        public ObservableCollection<PolylineGeometry> Geometries { get; set; } = new();

        public ObservableCollection<PolylineGeometry> RawGeometries { get; set; } = new();

        public string Filename { get; set; }

        public bool Collideable { get; set; }

        public WorldObjectType WorldObjectType { get; set; }

        private int DetermineZIndex(string type)
        {
            int result = 1;
            if (type == "crosswalk")
            {
                result = 5;
            }
            if (type == "tree")
            {
                result = 20;
            }

            return result;
        }

        private bool DetermineCollidablity(string type)
        {
            List<string> collideables = new List<string> { "boundary", "garage", "parking_bollard",
                "roadsign_parking_right", "roadsign_priority_stop", "roadsign_speed_40", "roadsign_speed_50", "roadsign_speed_60", "tree" };
            bool result = false;
            if (collideables.Contains(type))
            {
                result = true;
            }

            return result;
        }

        private WorldObjectType DetermineType(string type)
        {
            WorldObjectType result = WorldObjectType.Other;
            switch (type)
            {
                case "boundary":
                    result = WorldObjectType.Boundary;
                    break;
                case "garage":
                    result = WorldObjectType.Building;
                    break;
                case string s when s.StartsWith("car_"):
                    result = WorldObjectType.Car;
                    break;
                case "crosswalk":
                    result = WorldObjectType.Crosswalk;
                    break;
                case string s when s.StartsWith("parking_space_"):
                    result = WorldObjectType.ParkingSpace;
                    break;
                case string s when s.StartsWith("road_"):
                    result = WorldObjectType.Road;
                    break;
                case string s when s.StartsWith("roadsign_"):
                    result = WorldObjectType.RoadSign;
                    break;
                case "tree":
                    result = WorldObjectType.Tree;
                    break;
                default:
                    result = WorldObjectType.Other;
                    break;
            }

            return result;
        }

    }
}