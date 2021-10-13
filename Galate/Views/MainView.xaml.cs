using System.Windows;
using System.Windows.Input;

namespace Galate.Views
{
    public partial class MainView : Window
    {
        public MainView() => InitializeComponent();
        
        private void ThisWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
