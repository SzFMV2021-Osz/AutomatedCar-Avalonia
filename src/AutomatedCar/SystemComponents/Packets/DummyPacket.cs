using ReactiveUI;

namespace AutomatedCar.SystemComponents.Packets {
    public class DummyPacket : ReactiveObject, IReadOnlyDummyPacket {
        private int distanceX;
        private int distanceY;
        public int DistanceX {
            get => distanceX;
            set => this.RaiseAndSetIfChanged (ref distanceX, value);
        }
        public int DistanceY {
            get => distanceY;
            set => this.RaiseAndSetIfChanged (ref distanceY, value);
        }
    }
}