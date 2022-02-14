namespace AutomatedCar.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;
    using ReactiveUI;
    using Helpers;
    using Visualization;
    using Avalonia.Media;
    using Models;

    public class WorldViewModel : ViewModelBase
    {

        private readonly World world;
        private ObservableCollection<AutomatedCar> controlledCars = new();
        public ObservableCollection<WorldObject> WorldObjects { get; } = new ObservableCollection<WorldObject>();
        public WorldViewModel(World _world)
        {
            this.world = _world;
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
