using System.Collections.ObjectModel;
using AutomatedCar.Models;

using ReactiveUI;

namespace AutomatedCar.ViewModels
{

    using Models;
    using Visualization;

    public class CourseDisplayViewModel : ViewModelBase
    {
        // public readonly World world;
        private ObservableCollection<AutomatedCar> controlledCars = new();
        public ObservableCollection<WorldObject> WorldObjects { get; } = new ObservableCollection<WorldObject>();
        public CourseDisplayViewModel(World world)
        {
            WorldObjects = world.WorldObjects;
            Width = world.Width;
            Height = world.Height;

            // this.World = world;
        }

        // public World World { get; private set; }


        public void NextControlledCar()
        {
            // this.controlledCars = world.getNextControlledCar();
            this.RaisePropertyChanged("ControlledCar");
        }
        public void PrevControlledCar()
        {
            // this.controlledCars = world.getPreviousControlledCar();
            this.RaisePropertyChanged("ControlledCar");
        }

        public int Width { get; set; }

        public int Height { get; set; }

        private DebugStatus debugStatus = new DebugStatus();

        public DebugStatus DebugStatus
        {
            get => this.debugStatus;
            set => this.RaiseAndSetIfChanged(ref this.debugStatus, value);
        }
    }
}