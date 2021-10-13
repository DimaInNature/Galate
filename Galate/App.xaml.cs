using Galate.Services;
using Galate.ViewModels;
using Galate.Views;
using System.Windows;

namespace Galate
{
    public partial class App : Application
    {
        public DisplayWindowService DisplayWindow { get; private set; } = new DisplayWindowService();

        public App()
        {
            RegisterWindows();
        }

        private void RegisterWindows()
        {
            DisplayWindow.RegisterWindow<AdminMainViewModel, AdminMainView>();
            DisplayWindow.RegisterWindow<MainViewModel, MainView>();
            DisplayWindow.RegisterWindow<AuthorizationViewModel, AuthorizationView>();
            DisplayWindow.RegisterWindow<RegistrationViewModel, RegistrationView>();
        }
    }
}
