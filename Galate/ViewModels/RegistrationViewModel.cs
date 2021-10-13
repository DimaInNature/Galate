using Galate.Data;
using Galate.Models;
using Galate.Services;
using Galate.Services.Command;
using Galate.ViewModels.Base;
using Galate.Views;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Galate.ViewModels
{
    public sealed class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel() => InitialCommands();
        
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

        private string _login = string.Empty;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _password = string.Empty;

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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _name = string.Empty;

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }

        private string _surname = string.Empty;

        public string Mail
        {
            get => _mail;
            set
            {
                _mail = value;
                OnPropertyChanged("Mail");
            }
        }

        private string _mail = string.Empty;

        #endregion

        #region Command

        public ICommand RegistrationButtonClickCommand { get; private set; }

        public ICommand BackButtonClickCommand { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Command

        private void RegistrationButtonClick(object obj)
        {
            if(FieldsIsNotEmpty())
            {
                if (ValidationService.MailIsValid(Mail))
                {
                    var newUser = new UserModel() {Name = Name, Surname = Surname,
                        Login = Login, Password = Password, Email = Mail, IsAdmin = false };

                    if (DataManager.User.Create(newUser))
                    {
                        var view = obj as RegistrationView;

                        var displayRootRegistry = (Application.Current as App).DisplayWindow;

                        displayRootRegistry.Show(new MainViewModel());

                        view.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не был создан", "Ошибка создания",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Почта имеет неверный формат", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }       
        }

        private void BackButtonClick(object obj)
        {
            var view = obj as RegistrationView;

            var displayRootRegistry = (Application.Current as App).DisplayWindow;

            displayRootRegistry.Show(new AuthorizationViewModel());

            view.Close();
        }

        #endregion

        private void InitialCommands()
        {
            RegistrationButtonClickCommand = new DelegateCommandService(RegistrationButtonClick);
            BackButtonClickCommand = new DelegateCommandService(BackButtonClick);
        }

        private bool FieldsIsNotEmpty() => Name != string.Empty && Surname != string.Empty && Login != string.Empty
                && Password != string.Empty && Mail != string.Empty;

        #endregion
    }
}
