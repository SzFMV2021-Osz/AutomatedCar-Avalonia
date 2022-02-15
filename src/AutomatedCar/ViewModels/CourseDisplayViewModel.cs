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
        public ObservableCollection<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();

        public CourseDisplayViewModel(World world)
        {
            this.WorldObjects = new ObservableCollection<WorldObjectViewModel>(world.WorldObjects.Select(wo => new WorldObjectViewModel(wo)));
            Width = world.Width;
            Height = world.Height;
        }

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