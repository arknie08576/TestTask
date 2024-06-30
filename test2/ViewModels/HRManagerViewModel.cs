using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using test2.View;
using test2.ViewModels;
using test2;
using System.ComponentModel;

namespace test2.ViewModels
{
    public class HRManagerViewModel : INotifyPropertyChanged
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
        public HRManagerViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
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
        {
            _windowService.ShowWindow<ProjectsViewModel>();
        }
        private void OnLeaveRequests(object parameter)
        {
            _windowService.ShowWindow<LeaveRequestsViewModel>();
        }
        private void OnEmployes(object parameter)
        {

            _windowService.ShowWindow<EmployesViewModel>();
            
        }
        private void OnApprovalRequests(object parameter)
        {
            _windowService.ShowWindow<ApprovalRequestsViewModel>();
        }
        private void OnLogout(object parameter)
        {
            AuthenticationHelper.loggedUser = null;
            _windowService.ShowWindow<MainViewModel>();
            _windowService.CloseWindow<HRManagerViewModel>();
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
