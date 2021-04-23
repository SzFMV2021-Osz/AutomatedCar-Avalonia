namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Media;

    public class RadarSensor : SystemComponent
    {
        private readonly PolylineGeometry geometry;
        private AutomatedCar car;

        public RadarSensor(AutomatedCar automatedCar)
            : base(automatedCar.VirtualFunctionBus)
        {
            this.car = automatedCar;
            this.geometry = new PolylineGeometry(new List<Point> { new Point(54, 10), new Point(-54, -240), new Point(162, -240), new Point(54, 10) }, false);
            this.virtualFunctionBus = automatedCar.VirtualFunctionBus;
            automatedCar.VirtualFunctionBus.RegisterComponent(this);
        }

        public Geometry Geometry { get => this.geometry; }

        public PolylineGeometry AboluteGeometry { get => new PolylineGeometry(this.GetAbsolutePoints(), false); }

        public override void Process()
        {
            foreach (var wo in World.Instance.WorldObjects)
            {
                if (wo.Filename != "circle.png" && wo.WorldObjectType != WorldObjectType.Tree && wo.WorldObjectType != WorldObjectType.RoadSgin)
                {
                    continue;
                }

                foreach (var g in wo.Geometries)
                {
                    if (this.IsGeometryPointWithinSensorTriange(g, wo.X, wo.Y))
                    {
                        System.Console.WriteLine($"type: {wo.Filename}, x: {wo.X}, y: {wo.Y}");
                    }
                }
            }
        }

        private Avalonia.Points GetAbsolutePoints()
        {
            Avalonia.Points result = new ();
            foreach (Point p in this.geometry.Points)
            {
                result.Add(new Point(this.car.X + p.X, this.car.Y + p.Y));
            }

            return result;
        }

        private bool IsGeometryPointWithinSensorTriange(PolylineGeometry geometry, int x, int y)
        {
            bool result = false;
            var triangle = this.GetAbsolutePoints();
            foreach (var p in geometry.Points)
            {
                if (this.IsInside(triangle[0].X, triangle[0].Y, triangle[1].X, triangle[1].Y, triangle[2].X, triangle[2].Y, x + p.X, y + p.Y))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        // https://www.crazyforcode.com/check-point-lies-triangle/
        private double Area(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            return Math.Abs(((x1 * (y2 - y3)) + (x2 * (y3 - y1)) + (x3 * (y1 - y2))) / 2.0);
        }

        // https://www.crazyforcode.com/check-point-lies-triangle/
        private bool IsInside(double x1, double y1, double x2, double y2, double x3, double y3, double x, double y)
        {
            /* Calculate area of triangle ABC */
            double a = this.Area(x1, y1, x2, y2, x3, y3);

            /* Calculate area of triangle PBC */
            double a1 = this.Area(x, y, x2, y2, x3, y3);

            /* Calculate area of triangle PAC */
            double a2 = this.Area(x1, y1, x, y, x3, y3);

            /* Calculate area of triangle PAB */
            double a3 = this.Area(x1, y1, x2, y2, x, y);

            /* Check if sum of a1, a2 and a3 is same as a */
            return a.AlmostEqual(a1 + a2 + a3, 1e-06);
        }
    }
}