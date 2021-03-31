namespace AutomatedCar
{
    using System;
    using Avalonia.Threading;

    public abstract class GameBase
    {
        public const int TicksPerSecond = 60;

        private readonly DispatcherTimer timer =
            new DispatcherTimer() {Interval = new TimeSpan(0, 0, 0, 0, 1000 / TicksPerSecond)};

        public long CurrentTick { get; private set; }

        void DoTick()
        {
            this.Tick();
            this.CurrentTick++;
        }

        protected abstract void Tick();

        protected GameBase()
        {
            this.timer.Tick += delegate { DoTick(); };
        }

        public void Start() => this.timer.IsEnabled = true;

        public void Stop() => this.timer.IsEnabled = false;
    }
}