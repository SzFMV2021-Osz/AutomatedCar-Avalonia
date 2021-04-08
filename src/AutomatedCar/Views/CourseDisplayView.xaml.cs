namespace AutomatedCar.Views
{
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;

    public class CourseDisplayView : UserControl
    {
        public CourseDisplayView()
        {
            this.InitializeComponent();
            // In Avalonia, this is required to catch a XAML object
            var scrollViewer = this.FindControl<ScrollViewer>("scrollViewer");
            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            // scrollViewer.ScrollToHorizontalOffset is not implemented in Avalonia
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // System.Console.WriteLine(e.OffsetDelta);
        }
    }
}