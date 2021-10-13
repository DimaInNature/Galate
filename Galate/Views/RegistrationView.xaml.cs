using Galate.ViewModels;
using System.Windows;

namespace Galate.Views
{
    public partial class RegistrationView : Window
    {
        public RegistrationView() => InitializeComponent();

        private RegistrationViewModel _viewModel => (RegistrationViewModel)DataContext;
        
        private void UserPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = UserPasswordBox.Password;
            _viewModel.SecurePassword = UserPasswordBox.SecurePassword;
        }
    }
}
