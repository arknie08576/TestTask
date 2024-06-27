using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using test2.View;
using test2;
using System.Collections.ObjectModel;
using test2.Models;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using test2.ViewModels;

namespace test2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public MainViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;


            _windowService = windowService;

            // Initialize commands
            LoginCommand = new RelayCommand<object>(OnLogin);
            RegisterCommand = new RelayCommand<object>(OnRegister);
            //CloseCommand = new RelayCommand<object>(Close);
        }
        private void OnLogin(object parameter)
        {
            string username = Username;
            string password = Password;

            if (AuthenticationHelper.AuthenticateUser(username, password))
            {
                Position role = Position.Employee;
                _dialogService.ShowMessage("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


                var partner = context.Employes.Where(x => x.Username == username).Select(x => x.Position).FirstOrDefault();

                role = partner;





                switch (role)
                {
                    case Position.Employee:

                        _windowService.ShowWindow<EmployeeViewModel>();

                        break;
                    case Position.HRManager:

                        _windowService.ShowWindow<HRManagerViewModel>();
                        break;
                    case Position.ProjectManager:

                        _windowService.ShowWindow<ProjectManagerViewModel>();
                        break;
                    case Position.Administrator:

                        _windowService.ShowWindow<AdministratorViewModel>();
                        break;



                }




                _windowService.CloseWindow<MainViewModel>();
            }
            else
            {
                _dialogService.ShowMessage("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnRegister(object parameter)
        {
            _windowService.ShowWindow<RegisterViewModel>();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
