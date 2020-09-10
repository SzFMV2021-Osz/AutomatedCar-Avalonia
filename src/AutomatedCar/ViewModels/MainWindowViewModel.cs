using System;
using System.Collections.Generic;
using System.Text;
using AutomatedCar.Models;
using AutomatedCar.Views;
using ReactiveUI;

namespace AutomatedCar.ViewModels {
    using Models;

    public class MainWindowViewModel : ViewModelBase {
        private ViewModelBase dashboard;
        private ViewModelBase courseDisplay;
        private World world;

        public World World {
            get => world;
            private set => this.RaiseAndSetIfChanged (ref world, value);
        }

        public MainWindowViewModel (World world) {
            CourseDisplay = new CourseDisplayViewModel (world);
            Dashboard = new DashboardViewModel (world.ControlledCar);
            World = world;
        }

        public ViewModelBase CourseDisplay {
            get => courseDisplay;
            private set => this.RaiseAndSetIfChanged (ref courseDisplay, value);
        }
        public ViewModelBase Dashboard {
            get => dashboard;
            private set => this.RaiseAndSetIfChanged (ref dashboard, value);
        }
    }
}