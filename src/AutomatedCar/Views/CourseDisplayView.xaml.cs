namespace AutomatedCar.Views
{
    using Avalonia.Controls;
    using Avalonia.Input;
    using Avalonia.Markup.Xaml;

    public class CourseDisplayView : UserControl
    {
        public CourseDisplayView()
        {
            this.InitializeComponent();
            // In Avalonia, this is required to catch a XAML object
            // var scrollViewer = this.FindControl<ScrollViewer>("scrollViewer");
            // var scrollViewer = this.Get<ScrollViewer>("scrollViewer");
            // scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            // scrollViewer.ScrollToHorizontalOffset is not implemented in Avalonia

            // scrollViewer.Offset = new Avalonia.Vector(2500, 1000);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // System.Console.WriteLine(e.OffsetDelta);
        }

        // protected override void OnKeyDown(KeyEventArgs e)
        // {
        //     base.OnKeyDown(e);
        //     System.Console.WriteLine(this.Get<ScrollViewer>("scrollViewer").Width);
        // }
    }
}