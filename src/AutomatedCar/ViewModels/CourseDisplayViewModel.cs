using AutomatedCar.Models;

namespace AutomatedCar.ViewModels {
    using Models;

    public class CourseDisplayViewModel : ViewModelBase {
        public World World { get; private set; }

        public CourseDisplayViewModel (World world) {
            this.World = world;
        }
    }
}