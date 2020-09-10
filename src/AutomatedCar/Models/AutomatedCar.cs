using AutomatedCar.SystemComponents;
using Avalonia.Media;

namespace AutomatedCar.Models {
    using SystemComponents;

    public class AutomatedCar : Car {
        private VirtualFunctionBus virtualFunctionBus;
        private DummySensor dummySensor;

        public AutomatedCar (int x, int y, string filename) : base (x, y, filename) {
            virtualFunctionBus = new VirtualFunctionBus ();
            dummySensor = new DummySensor (virtualFunctionBus);
            Brush = new SolidColorBrush (Color.Parse ("red"));
        }

        public VirtualFunctionBus VirtualFunctionBus { get => virtualFunctionBus; }
        public Geometry Geometry { get; set; }
        public SolidColorBrush Brush { get; private set; }

        public void Start () {
            virtualFunctionBus.Start ();
        }
        public void Stop () {
            virtualFunctionBus.Stop ();
        }

    }
}