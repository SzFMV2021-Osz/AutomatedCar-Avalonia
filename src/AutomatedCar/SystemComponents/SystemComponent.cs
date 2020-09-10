namespace AutomatedCar.SystemComponents {
    public abstract class SystemComponent {
        protected VirtualFunctionBus virtualFunctionBus;

        protected SystemComponent (VirtualFunctionBus virtualFunctionBus) {
            this.virtualFunctionBus = virtualFunctionBus;
            virtualFunctionBus.registerComponent (this);
        }

        public abstract void Process ();
    }
}