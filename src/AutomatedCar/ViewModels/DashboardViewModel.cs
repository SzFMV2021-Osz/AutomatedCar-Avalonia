namespace AutomatedCar.ViewModels
{
    using AutomatedCar.Models;

    public class DashboardViewModel : ViewModelBase
    {
        public AutomatedCar ControlledCar { get; set; }
        public DashboardViewModel(AutomatedCar controlledCar)
        {
            this.ControlledCar = controlledCar;
        }
    }
}