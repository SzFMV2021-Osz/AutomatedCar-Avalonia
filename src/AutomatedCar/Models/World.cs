﻿namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;
    using ReactiveUI;
    using Helpers;
    using Visualization;
    using Avalonia.Media;

    public class World : ReactiveObject
    {
        // private static readonly System.Lazy<World> lazySingleton = new System.Lazy<World> (() => new World());
        // public static World Instance { get { return lazySingleton.Value; } }

        private AutomatedCar controlledCar;

        public static World Instance { get; } = new World();

        public ObservableCollection<WorldObject> WorldObjects { get; } = new ObservableCollection<WorldObject>();

        public AutomatedCar ControlledCar
        {
            get => this.controlledCar;
            set => this.RaiseAndSetIfChanged(ref this.controlledCar, value);
        }

        public int Width { get; set; }

        public int Height { get; set; }

        private DebugStatus debugStatus = new DebugStatus();

        public DebugStatus DebugStatus
        {
            get => this.debugStatus;
            set => this.RaiseAndSetIfChanged(ref this.debugStatus, value);
        }

        public void AddObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }

        public void PopulateFromJSON(string filename)
        {
            var rotationPoints = this.ReadRotationsPoints();
            var renderTransformOrigins = this.CalculateRenderTransformOrigins();
            var worldObjectPolygons = this.ReadPolygonJSON();

            StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream(filename));

            RawWorld rawWorld = JsonConvert.DeserializeObject<RawWorld>(reader.ReadToEnd());
            this.Height = rawWorld.Height;
            this.Width = rawWorld.Width;
            foreach (RawWorldObject rwo in rawWorld.Objects)
            {
                var wo = new WorldObject(rwo.X, rwo.Y, rwo.Type + ".png", this.DetermineZIndex(rwo.Type), this.DetermineCollidablity(rwo.Type), this.DetermineType(rwo.Type));
                (int x, int y) rp = (0, 0);

                if (rotationPoints.ContainsKey(rwo.Type))
                {
                    rp = rotationPoints[rwo.Type];
                }

                wo.RotationPoint = new System.Drawing.Point(rp.x, rp.y);

                string rto = "0,0";

                if (renderTransformOrigins.ContainsKey(rwo.Type))
                {
                   rto = renderTransformOrigins[rwo.Type];
                }

                wo.RenderTransformOrigin = rto;

                wo.Rotation = this.RotationMatrixToDegree(rwo.M11, rwo.M12);

                if (worldObjectPolygons.ContainsKey(rwo.Type))
                {
                    // fucking deep copy
                    foreach (var g in worldObjectPolygons[rwo.Type])
                    {
                        wo.Geometries.Add(new PolylineGeometry(g.Points, false));
                        wo.RawGeometries.Add(new PolylineGeometry(g.Points, false));
                    }
                }

                this.AddObject(wo);
            }
        }

        private List<System.Drawing.PointF> ToDotNetPoints(Avalonia.Points points)
        {
            var result = new List<System.Drawing.PointF>();
            foreach (var p in points)
            {
                result.Add(new PointF(Convert.ToSingle(p.X), Convert.ToSingle(p.Y)));
            }

            return result;
        }

        private List<System.Drawing.PointF> ToDotNetPoints(Avalonia.Points points, int x, int y)
        {
            var result = new List<System.Drawing.PointF>();
            foreach (var p in points)
            {
                result.Add(new PointF(Convert.ToSingle(p.X) + x, Convert.ToSingle(p.Y) + y));
            }

            return result;
        }

        private Avalonia.Points ToAvaloniaPoints(IEnumerable<PointF> points)
        {
            var result = new Avalonia.Points();
            foreach (var p in points)
            {
                result.Add(new Avalonia.Point(p.X, p.Y));
            }

            return result;
        }

        private Dictionary<string, (int x, int y)> ReadRotationsPoints(string filename = "reference_points.json")
        {
            StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream($"AutomatedCar.Assets.{filename}"));

            var rotationPoints = JsonConvert.DeserializeObject<List<RotationPoint>>(reader.ReadToEnd());
            Dictionary<string, (int x, int y)> result = new ();
            foreach (RotationPoint rp in rotationPoints)
            {
                result.Add(rp.Type, (rp.X, rp.Y));
            }

            return result;
        }

        private Dictionary<string, List<PolylineGeometry>> ReadPolygonJSON(string filename = "worldobject_polygons.json")
        {
            // TODO: Avalonia specific
            StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream($"AutomatedCar.Assets.{filename}"));

            var objects = JsonConvert.DeserializeObject<Dictionary<string, List<RawWorldObjectPolygon>>>(reader.ReadToEnd())["objects"];
            var result = new Dictionary<string, List<PolylineGeometry>>();
            foreach (RawWorldObjectPolygon rwop in objects)
            {
                var polygonList = new List<PolylineGeometry>();
                foreach (RawPolygon rp in rwop.Polys)
                {
                    var points = new Avalonia.Points();

                    foreach (var p in rp.Points)
                    {
                        points.Add(new Avalonia.Point(p[0], p[1]));
                    }

                    polygonList.Add(new PolylineGeometry(points, false));
                }

                result.Add(rwop.Type, polygonList);
            }

            return result;
        }

        // It accepts different string values than WPF. For .5,.5 you actually need 50%,50%. .5,.5 is treated as "half of the logical pixel" in both directions instead of "half of the control"
        private Dictionary<string, string> CalculateRenderTransformOrigins(string filename = "reference_points.json")
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream($"AutomatedCar.Assets.{filename}"));

            var rotationPoints = JsonConvert.DeserializeObject<List<RotationPoint>>(reader.ReadToEnd());
            Dictionary<string, string> result = new ();
            foreach (RotationPoint rp in rotationPoints)
            {
                var img = new System.Drawing.Bitmap(Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream($"AutomatedCar.Assets.WorldObjects.{rp.Type}.png"));
                var x = rp.Y / (double)img.Size.Width * 100.0;
                var y = rp.X / (double)img.Size.Height * 100.0;
                result.Add(rp.Type, x.ToString("0.00", nfi) + "%," + y.ToString("0.00", nfi) + "%");
            }

            return result;
        }

        // https://math.stackexchange.com/questions/3349681/angle-from-2x2-rotation-matrix
        // https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions
        private double RotationMatrixToDegree(float m11, float m12)
        {
            // return Math.Atan2(m11, m12) * (180.0 / Math.PI);
            var result = Math.Acos(m11) * (180.0 / Math.PI);
            if (m12 < 0)
            {
                result = 360 - result;
            }

            return result;
        }

        private int DetermineZIndex(string type)
        {
            int result = 1;
            if (type == "crosswalk")
            {
                result = 5;
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
                    result = WorldObjectType.RoadSgin;
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

        public GraphicsPath AddGeometry()
        {
            GraphicsPath geom = new ();
            List<Point> points = new ();
            points.Add(new Point(50, 50));
            points.Add(new Point(50, 100));
            points.Add(new Point(100, 50));
            points.Add(new Point(50, 50));
            geom.AddPolygon(points.ToArray());
            geom.CloseFigure();

            // geom.PathPoints

            return geom;
        }
    }
}
