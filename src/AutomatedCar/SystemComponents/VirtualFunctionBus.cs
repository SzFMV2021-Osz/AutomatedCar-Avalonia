using System.Collections.Generic;
using AutomatedCar.SystemComponents.Packets;

namespace AutomatedCar.SystemComponents {
    public class VirtualFunctionBus : GameBase {

        private List<SystemComponent> components = new List<SystemComponent> ();

        public IReadOnlyDummyPacket DummyPacket { get; set; }

        public void registerComponent (SystemComponent component) {
            components.Add (component);
        }
        protected override void Tick () {
            foreach (SystemComponent component in components) {
                component.Process ();
            }
        }
    }
}