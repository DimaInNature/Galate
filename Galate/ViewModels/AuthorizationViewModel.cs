using Galate.Data;
using Galate.Services;
using Galate.Services.Command;
using Galate.ViewModels.Base;
using Galate.Views;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace Galate.ViewModels
{
    class AuthorizationViewModel : BaseViewModel
    {
        public AuthorizationViewModel() => InitialCommands();
       
        #region Properties

        #region User prop

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _login;

        public unsafe string Password
        {
            [SecurityCritical]
            get
            {
                if (SecurePassword != null)
                {
                    SecureString securePassword = SecurePassword;
                    IntPtr intPtr = Marshal.SecureStringToBSTR(securePassword);
                    return new string((char*)(void*)intPtr);
                }
                return string.Empty;
            }
            [SecurityCritical]
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                using (SecureString secureString = new SecureString())
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        secureString.AppendChar(value[i]);
                    }
                }
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _password;

        public SecureString SecurePassword
        {
            get => _securePassword;
            set
            {
                _securePassword = value;
                OnPropertyChanged("SecurePassword");
            }
        }

        private SecureString _securePassword;

        #endregion

        #region Command

        public ICommand AuthorizationButtonClickCommand { get; private set; }

        public ICommand RegistrationButtonClickCommand { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Command

        private void AuthorizationButtonClick(object obj)
        {
            if (FieldsIsNotEmpty())
            {
                if (AuthorizationService.UserIsExist(Login, Password))
                {
                    var activeUser = DataManager.User.GetByLoginAndPassword(Login, Password);

                    if (activeUser.IsAdmin)
                    {
                        var view = obj as AuthorizationView;

                        var displayRootRegistry = (Application.Current as App).DisplayWindow;

                        displayRootRegistry.Show(new AdminMainViewModel());

                        view.Close();
                    }
                    else
                    {
                        var view = obj as AuthorizationView;

                        var displayRootRegistry = (Application.Current as App).DisplayWindow;

                        displayRootRegistry.Show(new MainViewModel());

                        view.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Пользователя с таким логином и паролем несуществует", "Ошибка ввода данных",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните поля ввода логина и пароля.", "Ошибка ввода данных",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegistrationButtonClick(object obj)
        {
            var view = obj as AuthorizationView;

            var displayRootRegistry = (Application.Current as App).DisplayWindow;

            displayRootRegistry.Show(new RegistrationViewModel());

            view.Close();
        }

        #endregion

        private void InitialCommands()
        {
            AuthorizationButtonClickCommand = new DelegateCommandService(AuthorizationButtonClick);
            RegistrationButtonClickCommand = new DelegateCommandService(RegistrationButtonClick);
        }

        private bool FieldsIsNotEmpty() => Login != string.Empty && Password != string.Empty;

        #endregion
    }
}
