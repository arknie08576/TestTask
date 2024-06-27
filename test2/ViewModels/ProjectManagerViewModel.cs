using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using test2.View;

namespace test2.ViewModels
{
    public class ProjectManagerViewModel : INotifyPropertyChanged
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ProjectsCommand { get; }
        public ICommand LeaveRequestsCommand { get; }
        public ICommand EmployesCommand { get; }
        public ICommand ApprovalRequestsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ProjectManagerViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;


            _windowService = windowService;

            // Initialize commands
            ProjectsCommand = new RelayCommand<object>(OnProcjects);
            LeaveRequestsCommand = new RelayCommand<object>(OnLeaveRequests);
            EmployesCommand = new RelayCommand<object>(OnEmployes);
            ApprovalRequestsCommand = new RelayCommand<object>(OnApprovalRequests);
            LogoutCommand = new RelayCommand<object>(OnLogout);
            //CloseCommand = new RelayCommand<object>(Close);
        }
        private void OnProcjects(object parameter)
        { }
        private void OnLeaveRequests(object parameter)
        { }
        private void OnEmployes(object parameter)
        { }
        private void OnApprovalRequests(object parameter)
        { }
        private void OnLogout(object parameter)
        {
            AuthenticationHelper.loggedUser = null;
            _windowService.ShowWindow<MainViewModel>();
            _windowService.CloseWindow<ProjectManagerViewModel>();
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
