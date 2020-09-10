using AutomatedCar.Models;
using ReactiveUI;

namespace AutomatedCar.ViewModels {
    using Models;

    public class DashboardViewModel : ViewModelBase {
        public AutomatedCar controlledCar;

        public DashboardViewModel (Models.AutomatedCar controlledCar) {
            ControlledCar = controlledCar;
        }

        public Models.AutomatedCar ControlledCar {
            get => controlledCar;
            private set => this.RaiseAndSetIfChanged (ref controlledCar, value);
        }
    }
}