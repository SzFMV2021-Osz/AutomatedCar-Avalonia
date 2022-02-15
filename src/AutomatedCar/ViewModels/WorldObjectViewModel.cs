namespace AutomatedCar.ViewModels
{
    using AutomatedCar.Models;
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WorldObjectViewModel : ViewModelBase
    {
        private WorldObject worldObject;

        public WorldObjectViewModel(WorldObject worldObject)
        {
            this.worldObject = worldObject;
        }

        public int X
        {
            get => worldObject.X;
            //set => this.RaiseAndSetIfChanged(ref this.worldObject.X, value);
        }

        public int Y
        {
            get => worldObject.Y;
        }

        public double Rotation
        {
            get => worldObject.Rotation;
        }

        public int ZIndex 
        {
            get => worldObject.ZIndex;
        }

        public string Filename
        {
            get => worldObject.Filename;
        }
    }
}
