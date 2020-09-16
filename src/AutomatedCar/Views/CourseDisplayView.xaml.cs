namespace AutomatedCar.Views
{
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;

    public class CourseDisplayView : UserControl
    {
        public CourseDisplayView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}