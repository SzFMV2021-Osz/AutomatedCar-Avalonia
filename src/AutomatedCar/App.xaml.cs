namespace AutomatedCar
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Reflection;
    using AutomatedCar.Models;
    using AutomatedCar.ViewModels;
    using AutomatedCar.Views;
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Markup.Xaml;
    using Avalonia.Media;
    using Newtonsoft.Json.Linq;

    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream($"AutomatedCar.Assets.worldobject_polygons.json"));
                string json_text = reader.ReadToEnd();
                dynamic stuff = JObject.Parse(json_text);
                var points = new List<Point>();
                foreach (var i in stuff["objects"][0]["polys"][0]["points"])
                {
                    points.Add(new Point(i[0].ToObject<int>(), i[1].ToObject<int>()));
                }

                var geom = new PolylineGeometry(points, false);

                var world = World.Instance;
                var circle = new Circle(200, 200, "circle.png", 20);
                circle.Width = 40;
                circle.Height = 40;
                circle.ZIndex = 20;
                circle.Rotation = 45;
                world.AddObject(circle);

                world.PopulateFromJSON($"AutomatedCar.Assets.test_world.json");

                var controlledCar = new Models.AutomatedCar(480, 1425, "car_1_white.png");
                controlledCar.Geometry = geom;
                controlledCar.RotationPoint = new System.Drawing.Point(54, 120);
                controlledCar.Geometries = new ObservableCollection<PolylineGeometry>();
                controlledCar.Geometries.Add(new PolylineGeometry(new List<Point> { new Point(36, 240), new Point(36, 180) }, false));
                controlledCar.Geometries.Add(new PolylineGeometry(new List<Point> { new Point(72, 240), new Point(72, 180) }, false));
                world.AddControlledCar(controlledCar);
                controlledCar.Start();

                var controlledCar2 = new Models.AutomatedCar(4250, 1420, "car_1_red.png");
                controlledCar2.Geometry = geom;
                controlledCar2.RotationPoint = new System.Drawing.Point(54, 120);
                controlledCar2.Geometries = new ObservableCollection<PolylineGeometry>();
                controlledCar2.Geometries.Add(new PolylineGeometry(new List<Point> { new Point(36, 240), new Point(36, 180) }, false));
                controlledCar2.Geometries.Add(new PolylineGeometry(new List<Point> { new Point(72, 240), new Point(72, 180) }, false));
                controlledCar2.Rotation = -90;
                world.AddControlledCar(controlledCar2);
                controlledCar2.Start();

                // var game = new Game(world);
                // game.Start();

                // var pl = new PointF[] { new PointF(5, 5), new PointF(15, 5), new PointF(15, 15), new PointF(5, 15)};
                // var mx = new System.Drawing.Drawing2D.Matrix();
                // mx.RotateAt(45, new PointF(10f, 10f));
                // mx.TransformPoints(pl);
                // foreach (var p in pl)
                //     System.Console.WriteLine(p);
                // var pl2 = new PointF[] { new PointF(105, 105), new PointF(115, 105), new PointF(115, 115), new PointF(105, 115)};
                // var mx2 = new System.Drawing.Drawing2D.Matrix();
                // mx2.RotateAt(45, new PointF(10f, 10f));
                // mx2.TransformPoints(pl2);
                // foreach (var p in pl2)
                //     System.Console.WriteLine(p);
                // var pl3 = new PointF[] { new PointF(105, 105), new PointF(115, 105), new PointF(115, 115), new PointF(105, 115)};
                // var mx3 = new System.Drawing.Drawing2D.Matrix();
                // mx3.RotateAt(45, new PointF(100f, 100f));
                // mx3.TransformPoints(pl3);
                // foreach (var p in pl3)
                //     System.Console.WriteLine(p);
                // var p1 = new GeoLibrary.Model.Point(10, 10);
                // var p2 = new GeoLibrary.Model.Point(20, 20);
                // GeoLibrary.Model.Geometry gg = new GeoLibrary.Model.LineString(new List<GeoLibrary.Model.Point> {p1, p2});

                desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel(world) };
                ((MainWindow)desktop.MainWindow).FocusCar();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}