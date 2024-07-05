using System.Windows.Input;
using test2.Helpers;
using test2.Interfaces;
using test2.Commands;

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
            ProjectsCommand = new RelayCommand<object>(OnProjects);
            LeaveRequestsCommand = new RelayCommand<object>(OnLeaveRequests);
            LogoutCommand = new RelayCommand<object>(OnLogout);
            ChangePasswordCommand = new RelayCommand<object>(OnChangePassword);

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
