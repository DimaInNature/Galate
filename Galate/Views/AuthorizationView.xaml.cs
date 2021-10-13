using Galate.ViewModels;
using System.Windows;

namespace Galate.Views
{
    public partial class AuthorizationView : Window
    {
        public AuthorizationView() => InitializeComponent();

        private AuthorizationViewModel _viewModel => (AuthorizationViewModel)DataContext;
       
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = PasswordBox.Password;
            _viewModel.SecurePassword = PasswordBox.SecurePassword;
        }
    }
}
