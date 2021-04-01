namespace AutomatedCar.SystemComponents
{
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Media;
    using AutomatedCar.SystemComponents.Packets;

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

        public Geometry AboluteGeometry { get => new PolylineGeometry(this.GetAbsolutePoints(), false); }

        public override void Process()
        {
        }

        private List<Point> GetAbsolutePoints()
        {
            List<Point> result = new List<Point>();
            foreach (Point p in this.geometry.Points)
            {
                result.Add(new Point(this.car.X + p.X, this.car.Y + p.Y));
            }

            return result;
        }
    }
}