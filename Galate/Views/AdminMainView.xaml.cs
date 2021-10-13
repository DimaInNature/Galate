using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Galate.Views
{
    public partial class AdminMainView : Window
    {
        public AdminMainView() => InitializeComponent();

        private void ThisWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        // Решение проблемы с фокусом у календаря
        private void CreateTourCalendat_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is CalendarItem) Mouse.Capture(null);
        }
    }
}
