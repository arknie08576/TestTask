using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using test2.Views;
using test2.Helpers;
using System.Runtime.CompilerServices;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;

namespace test2.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {   
        private readonly IWindowService _windowService;
        public ICommand ProjectsCommand { get; }
        public ICommand LeaveRequestsCommand { get; }
        public ICommand ChangePasswordCommand { get; }
        public ICommand LogoutCommand { get; }
        
        
        public EmployeeViewModel(IWindowService windowService)
        {
            


            _windowService = windowService;

            // Initialize commands
            ProjectsCommand = new RelayCommand<object>(OnProjects);
            LeaveRequestsCommand = new RelayCommand<object>(OnLeaveRequests);
            LogoutCommand = new RelayCommand<object>(OnLogout);
            ChangePasswordCommand = new RelayCommand<object>(OnChangePassword);
            //CloseCommand = new RelayCommand<object>(Close);
        }
        private void OnProjects(object parameter)
        {
            _windowService.ShowWindow<ProjectsViewModel>();

        }
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
        private void OnChangePassword(object parameter)
        {
            _windowService.ShowWindow<ChangePasswordViewModel>();
        }
    }
}
