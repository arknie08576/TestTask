using System.Windows.Input;
using test2.Helpers;
using test2.Interfaces;
using test2.Commands;

namespace test2.ViewModels
{
    public class HRManagerViewModel : ViewModelBase
    {

        private readonly IWindowService _windowService;
        public ICommand ProjectsCommand { get; }
        public ICommand LeaveRequestsCommand { get; }
        public ICommand EmployesCommand { get; }
        public ICommand ApprovalRequestsCommand { get; }
        public ICommand ChangePasswordCommand { get; }
        public ICommand LogoutCommand { get; }

        public HRManagerViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            ProjectsCommand = new RelayCommand<object>(OnProcjects);
            LeaveRequestsCommand = new RelayCommand<object>(OnLeaveRequests);
            EmployesCommand = new RelayCommand<object>(OnEmployes);
            ApprovalRequestsCommand = new RelayCommand<object>(OnApprovalRequests);
            LogoutCommand = new RelayCommand<object>(OnLogout);
            ChangePasswordCommand = new RelayCommand<object>(OnChangePassword);
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
        private void OnChangePassword(object parameter)
        {
            _windowService.ShowWindow<ChangePasswordViewModel>();
        }
    }
}
