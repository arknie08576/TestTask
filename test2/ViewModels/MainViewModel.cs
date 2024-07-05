using System.Windows.Input;
using System.Windows;
using test2.Helpers;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;
using Microsoft.EntityFrameworkCore;

namespace test2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
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


            LoginCommand = new AsyncRelayCommand<object>(OnLoginAsync);
            RegisterCommand = new RelayCommand<object>(OnRegister);

        }
        private async Task OnLoginAsync(object parameter)
        {
            string username = Username;
            string password = Password;
            var isAuthenticated = await AuthenticationHelper.AuthenticateUserAsync(username, password);
            if (isAuthenticated)
            {
                Position role = Position.Employee;
                _dialogService.ShowMessage("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


                var partner = await context.Employes.Where(x => x.Username == username).Select(x => x.Position).FirstOrDefaultAsync();

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
    }
}
