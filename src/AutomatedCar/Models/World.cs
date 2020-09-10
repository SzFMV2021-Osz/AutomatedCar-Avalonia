﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace AutomatedCar.Models {
    public class World : ReactiveObject {

        // private static readonly System.Lazy<World> lazySingleton = new System.Lazy<World> (() => new World());

        // public static World Instance { get { return lazySingleton.Value; } }

        public static World Instance { get; } = new World ();

        public ObservableCollection<WorldObject> WorldObjects { get; } = new ObservableCollection<WorldObject> ();

        private AutomatedCar _controlledCar;
        public AutomatedCar ControlledCar {
            get => _controlledCar;
            set => this.RaiseAndSetIfChanged (ref _controlledCar, value);
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public World () { }
        public void addObject (WorldObject worldObject) {
            WorldObjects.Add (worldObject);
        }
    }
}