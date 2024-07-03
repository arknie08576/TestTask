using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using test2.Views;
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;

namespace test2.ViewModels
{
    public class AdministratorViewModel : ViewModelBase
    {
        
        private readonly IWindowService _windowService;
        public ICommand ProjectsCommand { get; }
        public ICommand LeaveRequestsCommand { get; }
        public ICommand EmployesCommand { get; }
        public ICommand ApprovalRequestsCommand { get; }
        public ICommand ChangePasswordCommand { get; }
        public ICommand LogoutCommand { get; }
        public AdministratorViewModel( IWindowService windowService)
        {
            
            


            _windowService = windowService;

            // Initialize commands
            ProjectsCommand = new RelayCommand<object>(OnProcjects);
            LeaveRequestsCommand = new RelayCommand<object>(OnLeaveRequests);
            EmployesCommand = new RelayCommand<object>(OnEmployes);
            ApprovalRequestsCommand = new RelayCommand<object>(OnApprovalRequests);
            LogoutCommand = new RelayCommand<object>(OnLogout);
            ChangePasswordCommand = new RelayCommand<object>(OnChangePassword);
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
            _windowService.CloseWindow<AdministratorViewModel>();
        }
        private void OnChangePassword(object parameter)
        {
            _windowService.ShowWindow<ChangePasswordViewModel>();
        }
    }
}
