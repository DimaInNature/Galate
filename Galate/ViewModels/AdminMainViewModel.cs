using Galate.Data;
using Galate.Models;
using Galate.Services.Command;
using Galate.ViewModels.Base;
using Galate.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Galate.ViewModels
{
    public sealed class AdminMainViewModel : BaseViewModel
    {
        public AdminMainViewModel()
        {
            InitialCommands();
            InitialDataCollection();
        }

        #region Properties

        #region Collections

        public List<TourModel> Tours
        {
            get => _tours;
            set
            {
                _tours = value;
                OnPropertyChanged("Tours");
            }
        }

        private List<TourModel> _tours;

        public List<ClientModel> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }

        private List<ClientModel> _clients;

        public List<AirTravelModel> AirTravels
        {
            get => _airTravels;
            set
            {
                _airTravels = value;
                OnPropertyChanged("AirTravels");
            }
        }

        private List<AirTravelModel> _airTravels;

        #endregion

        #region Create

        public DateTime CreateDate
        {
            get => _createDate;
            set
            {
                _createDate = value;
                OnPropertyChanged("CreateDate");
            }
        }

        private DateTime _createDate = new DateTime(DateTime.UtcNow.Year,
            DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, DateTimeKind.Utc);

        public string CreateClientName
        {
            get => _createClientName;
            set
            {
                _createClientName = value;
                OnPropertyChanged("CreateClientName");
            }
        }

        private string _createClientName = string.Empty;

        public string CreateClientSurname
        {
            get => _createClientSurname;
            set
            {
                _createClientSurname = value;
                OnPropertyChanged("CreateClientSurname");
            }
        }

        private string _createClientSurname = string.Empty;

        public string CreateClientPhone
        {
            get => _createClientPhone;
            set
            {
                _createClientPhone = value;
                OnPropertyChanged("CreateClientPhone");
            }
        }

        private string _createClientPhone = string.Empty;

        public string CreateTourFromCity
        {
            get => _createTourFromCity;
            set
            {
                _createTourFromCity = value;
                OnPropertyChanged("CreateTourFromCity");
            }
        }

        private string _createTourFromCity = string.Empty;

        public string CreateTourToCity
        {
            get => _createTourToCity;
            set
            {
                _createTourToCity = value;
                OnPropertyChanged("CreateTourToCity");
            }
        }

        private string _createTourToCity = string.Empty;

        public double CreateTourPrice
        {
            get => _createTourPrice;
            set
            {
                _createTourPrice = value;
                OnPropertyChanged("CreateTourPrice");
            }
        }

        private double _createTourPrice = 0;

        public DateTime CreateTourDate
        {
            get => _createTourDate;
            set
            {
                _createTourDate = value;
                OnPropertyChanged("CreateTourDate");
            }
        }

        private DateTime _createTourDate = new DateTime(DateTime.UtcNow.Year,
            DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, DateTimeKind.Utc);

        #endregion

        #region Selected

        public TourModel CreateSelectedTour
        {
            get => _createSelectedTour;
            set
            {
                _createSelectedTour = value;
                OnPropertyChanged("CreateSelectedTour");
            }
        }

        private TourModel _createSelectedTour;

        public ClientModel CreateSelectedClient
        {
            get => _createSelectedClient;
            set
            {
                _createSelectedClient = value;
                OnPropertyChanged("CreateSelectedClient");
            }
        }

        private ClientModel _createSelectedClient;

        public AirTravelModel ViewSelectedAirTravel
        {
            get => _viewSelectedAirTravel;
            set
            {
                _viewSelectedAirTravel = value;
                OnPropertyChanged("ViewSelectedAirTravel");
            }
        }

        private AirTravelModel _viewSelectedAirTravel;

        public ClientModel ViewSelectedClient
        {
            get => _viewSelectedClient;
            set
            {
                _viewSelectedClient = value;
                OnPropertyChanged("ViewSelectedClient");
            }
        }

        private ClientModel _viewSelectedClient;

        public ClientModel DeleteSelectedClient
        {
            get => _deleteSelectedClient;
            set
            {
                _deleteSelectedClient = value;
                OnPropertyChanged("DeleteSelectedClient");
            }
        }

        private ClientModel _deleteSelectedClient;

        public TourModel DeleteSelectedTour
        {
            get => _deleteSelectedTour;
            set
            {
                _deleteSelectedTour = value;
                OnPropertyChanged("DeleteSelectedTour");
            }
        }

        private TourModel _deleteSelectedTour;

        #endregion

        #region Command

        public ICommand LogoutButtonClickCommand { get; private set; }

        public ICommand CloseApplicationButtonClickCommand { get; private set; }

        public ICommand CreateAirTravelButtonCommand { get; private set; }

        public ICommand CancelAirTravelButtonCommand { get; private set; }

        public ICommand DeleteAirTravelButtonCommand { get; private set; }

        public ICommand DeleteClientsButtonCommand { get; private set; }

        public ICommand CreateClientsButtonCommand { get; private set; }

        public ICommand CreateTourButtonCommand { get; private set; }

        public ICommand DeleteTourButtonCommand { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Command

        private void LogoutButtonClick(object obj)
        {
            var view = obj as AdminMainView;

            var displayRootRegistry = (Application.Current as App).DisplayWindow;

            displayRootRegistry.Show(new AuthorizationViewModel());

            view.Close();
        }

        private void CloseApplicationButtonClick(object obj) => Application.Current.Shutdown();

        private void CreateAirTravelButton(object obj)
        {
            if(CreateSelectedClient != null && CreateSelectedTour != null)
            {
                var newAirTravel = new AirTravelModel()
                {
                    ClientId = CreateSelectedClient.Id,
                    TourId = CreateSelectedTour.Id
                };

                DataManager.AirTravel.Create(newAirTravel);
                _airTravels = DataManager.AirTravel.GetAll().ToList();
                OnPropertyChanged("AirTravels");
            }
            else
            {
                MessageBox.Show("Не выбран тур и/или клиент", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelAirTravelButton(object obj) => ClearFields();

        private void DeleteAirTravelButton(object obj)
        {
            if(ViewSelectedAirTravel != null)
            {
                if (DataManager.AirTravel.Delete(ViewSelectedAirTravel.Id))
                {
                    _airTravels = DataManager.AirTravel.GetAll().ToList();
                    OnPropertyChanged("AirTravels");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Не удалось удалить полёт", "Ошибка удаления",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Полёт не был выбран", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteClientsButton(object obj)
        {
            if(DeleteSelectedClient != null)
            {
                if (DataManager.Client.Delete(DeleteSelectedClient.Id))
                {
                    DataManager.AirTravel.DeleteByClientId(DeleteSelectedClient.Id);
                    _clients = DataManager.Client.GetAll().ToList();
                    OnPropertyChanged("Clients");
                    _airTravels = DataManager.AirTravel.GetAll().ToList();
                    OnPropertyChanged("AirTravels");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Не удалось удалить клиента", "Ошибка удаления",
                       MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Клиент не был выбран", "Ошибка ввода",
                       MessageBoxButton.OK, MessageBoxImage.Error);
            }  
        }

        private void CreateClientsButton(object obj)
        {
            if(CreateClientName != string.Empty && CreateClientSurname != string.Empty &&
                CreateClientPhone != string.Empty)
            {
                var newClient = new ClientModel()
                {
                    Name = CreateClientName,
                    Surname = CreateClientSurname,
                    Phone = CreateClientPhone
                };

                if (DataManager.Client.Create(newClient))
                {
                    _clients = DataManager.Client.GetAll().ToList();
                    OnPropertyChanged("Clients");
                }
                else
                {
                    MessageBox.Show("Клиент не был создан", "Ошибка создания",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateTourButton(object obj)
        {
            if(CreateTourFromCity != null && CreateTourToCity != null && (CreateTourPrice > 0))
            {
                var newTour = new TourModel()
                {
                    TourName = $"{CreateTourFromCity} - {CreateTourToCity}",
                    TourDate = CreateTourDate,
                    Price = CreateTourPrice
                };

                if (DataManager.Tour.Create(newTour))
                {
                    _tours = DataManager.Tour.GetAll().ToList();
                    OnPropertyChanged("Tours");
                }
                else
                {
                    MessageBox.Show("Тур не был создан", "Ошибка создания",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля были заполнены корректно", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }

        private void DeleteTourButton(object obj)
        {
            if(DeleteSelectedTour != null)
            {
                if (DataManager.Tour.Delete(DeleteSelectedTour.Id))
                {
                    DataManager.AirTravel.DeleteByTourId(DeleteSelectedTour.Id);
                    _airTravels = DataManager.AirTravel.GetAll().ToList();
                    OnPropertyChanged("AirTravels");
                    _tours = DataManager.Tour.GetAll().ToList();
                    OnPropertyChanged("Tours");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Не удалось удалить тур", "Ошибка удаления",
                       MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Тур не был выбран", "Ошибка ввода",
                       MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        private void InitialCommands()
        {
            LogoutButtonClickCommand = new DelegateCommandService(LogoutButtonClick);
            CloseApplicationButtonClickCommand = new DelegateCommandService(CloseApplicationButtonClick);
            CreateAirTravelButtonCommand = new DelegateCommandService(CreateAirTravelButton);
            CancelAirTravelButtonCommand = new DelegateCommandService(CancelAirTravelButton);
            DeleteAirTravelButtonCommand = new DelegateCommandService(DeleteAirTravelButton);
            DeleteClientsButtonCommand = new DelegateCommandService(DeleteClientsButton);
            CreateClientsButtonCommand = new DelegateCommandService(CreateClientsButton);
            CreateTourButtonCommand = new DelegateCommandService(CreateTourButton);
            DeleteTourButtonCommand = new DelegateCommandService(DeleteTourButton);
        }

        private void InitialDataCollection()
        {
            _tours = DataManager.Tour.GetAll().ToList();
            _clients = DataManager.Client.GetAll().ToList();
            _airTravels = DataManager.AirTravel.GetAll().ToList();
        }

        private void ClearFields()
        {
            _createSelectedTour = null;
            OnPropertyChanged("CreateSelectedTour");
            _createSelectedClient = null;
            OnPropertyChanged("CreateSelectedClient");
            _viewSelectedAirTravel = null;
            OnPropertyChanged("ViewSelectedAirTravel");
            _viewSelectedClient = null;
            OnPropertyChanged("ViewSelectedClient");
            _deleteSelectedClient = null;
            OnPropertyChanged("DeleteSelectedClient");
            _deleteSelectedTour = null;
            OnPropertyChanged("DeleteSelectedTour");
        }

        #endregion
    }
}
