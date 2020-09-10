using System.Collections.Generic;
using AutomatedCar.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace AutomatedCar.Views {
    public class CourseDisplayView : UserControl {
        public CourseDisplayView () {
            InitializeComponent ();
        }

        private void InitializeComponent () {
            AvaloniaXamlLoader.Load (this);
        }
    }
}