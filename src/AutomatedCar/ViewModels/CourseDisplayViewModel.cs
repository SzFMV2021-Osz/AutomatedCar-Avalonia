using System.Collections.ObjectModel;
using AutomatedCar.Models;
using System.Linq;

using ReactiveUI;

namespace AutomatedCar.ViewModels
{

    using Models;
    using Visualization;

    public class CourseDisplayViewModel : ViewModelBase
    {
        private ObservableCollection<AutomatedCar> controlledCars = new();
        public AutomatedCar ControlledCar { get; set; }
        public ObservableCollection<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();

        public CourseDisplayViewModel(World world)
        {
            this.WorldObjects = new ObservableCollection<WorldObjectViewModel>(world.WorldObjects.Select(wo => new WorldObjectViewModel(wo)));
            Width = world.Width;
            Height = world.Height;
            ControlledCar = world.ControlledCar;
        }

        public int Width { get; set; }

        public int Height { get; set; }

        private DebugStatus debugStatus = new DebugStatus();

        public DebugStatus DebugStatus
        {
            get => this.debugStatus;
            set => this.RaiseAndSetIfChanged(ref this.debugStatus, value);
        }

        public void NextControlledCar()
        {
            World.Instance.NextControlledCar();
        }
        public void PrevControlledCar()
        {
            World.Instance.PrevControlledCar();
        }

        public void KeyUp()
        {
            World.Instance.ControlledCar.Y -= 5;
        }
    }
}