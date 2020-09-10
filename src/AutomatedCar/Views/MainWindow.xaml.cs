using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace AutomatedCar.Views {
    public class MainWindow : Window {
        public MainWindow () {
            InitializeComponent ();
        }

        private void InitializeComponent () {
            AvaloniaXamlLoader.Load (this);
        }

        protected override void OnKeyDown (KeyEventArgs e) {
            Keyboard.Keys.Add (e.Key);
            base.OnKeyDown (e);
        }

        protected override void OnKeyUp (KeyEventArgs e) {
            Keyboard.Keys.Remove (e.Key);
            base.OnKeyUp (e);
        }
    }
}