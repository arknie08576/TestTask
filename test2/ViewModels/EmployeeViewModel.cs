using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using test2.View;
using test2;
using System.Runtime.CompilerServices;

namespace test2.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ProjectsCommand { get; }
        public ICommand LeaveRequestsCommand { get; }
        public ICommand LogoutCommand { get; }
        public EmployeeViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;


            _windowService = windowService;

            // Initialize commands
            ProjectsCommand = new RelayCommand<object>(OnProjects);
            LeaveRequestsCommand = new RelayCommand<object>(OnLeaveRequests);
            LogoutCommand = new RelayCommand<object>(OnLogout);
            //CloseCommand = new RelayCommand<object>(Close);
        }
        private void OnProjects(object parameter)
        { }
        private void OnLeaveRequests(object parameter)
        {
            _windowService.ShowWindow<LeaveRequestsViewModel>();

        }
        private void OnLogout(object parameter)
        {
            AuthenticationHelper.loggedUser = null;
            _windowService.ShowWindow<MainViewModel>();
            _windowService.CloseWindow<EmployeeViewModel>();
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
