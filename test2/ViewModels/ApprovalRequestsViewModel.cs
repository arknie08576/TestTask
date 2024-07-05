using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using test2.TableModels;
using test2.Helpers;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;
using Microsoft.EntityFrameworkCore;

namespace test2.ViewModels
{
    public class ApprovalRequestsViewModel : ViewModelBase
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        string user;
        public ICommand FilterCommand { get; }
        public ICommand NewEmployeeCommand { get; }
        public ICommand RowDoubleClickCommand { get; }
        public ApprovalRequestsViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;
            _windowService = windowService;
            FilterCommand = new AsyncRelayCommand<object>(OnFilterAsync);
            RowDoubleClickCommand = new RelayCommand<ViewApprovalRequest>(OnRowDoubleClick);
            Task.Run(LoadApprovalRequestsAsync);
        }
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _approver;
        public string Approver
        {
            get => _approver;
            set
            {
                _approver = value;
                OnPropertyChanged(nameof(Approver));
            }
        }
        private string _leaveRequestt;
        public string LeaveRequestt
        {
            get => _leaveRequestt;
            set
            {
                _leaveRequestt = value;
                OnPropertyChanged(nameof(LeaveRequestt));
            }
        }
        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        private ObservableCollection<ViewApprovalRequest> _filteredApprovalRequests;
        public ObservableCollection<ViewApprovalRequest> FilteredApprovalRequests
        {
            get { return _filteredApprovalRequests; }
            set
            {
                if (_filteredApprovalRequests != value)
                {
                    _filteredApprovalRequests = value;
                    OnPropertyChanged(nameof(FilteredApprovalRequests));
                }
            }
        }
        private async Task LoadApprovalRequestsAsync()
        {
            var approvalRequests = await context.ApprovalRequests.ToListAsync();
            var viewapprovalRequests = new List<ViewApprovalRequest>();
            foreach (var approvalRequest in approvalRequests)
            {
                var approver = await context.Employes.Where(x => x.Id == approvalRequest.Approver).Select(x => x.FullName).FirstOrDefaultAsync();
                ViewApprovalRequest p = new ViewApprovalRequest
                {
                    Id = approvalRequest.Id,
                    Approver = approver,
                    LeaveRequestt = approvalRequest.LeaveRequest,
                    Status = approvalRequest.Status,
                    Comment = approvalRequest.Comment




                };
                viewapprovalRequests.Add(p);


            }

            FilteredApprovalRequests = new ObservableCollection<ViewApprovalRequest>(viewapprovalRequests);


        }
        private async Task OnFilterAsync(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<ApprovalRequestsViewModel>();
                return;
            }

            var approvalRequests = await context.ApprovalRequests.ToListAsync();
            var viewapprovalRequests = new List<ViewApprovalRequest>();
            foreach (var approvalRequest in approvalRequests)
            {
                var approver = await context.Employes.Where(x => x.Id == approvalRequest.Approver).Select(x => x.FullName).FirstOrDefaultAsync();
                ViewApprovalRequest p = new ViewApprovalRequest
                {
                    Id = approvalRequest.Id,
                    Approver = approver,
                    LeaveRequestt = approvalRequest.LeaveRequest,
                    Status = approvalRequest.Status,
                    Comment = approvalRequest.Comment




                };
                viewapprovalRequests.Add(p);


            }



            if (!string.IsNullOrEmpty(Id))
            {

                viewapprovalRequests = viewapprovalRequests.Where(x => x.Id.ToString() == Id).ToList();
            }

            if (!string.IsNullOrEmpty(Approver))
            {

                viewapprovalRequests = viewapprovalRequests.Where(x => x.Approver == Approver).ToList();
            }
            if (!string.IsNullOrEmpty(LeaveRequestt))
            {

                viewapprovalRequests = viewapprovalRequests.Where(x => x.LeaveRequestt.ToString() == LeaveRequestt).ToList();
            }





            ApprovalRequestStatus x = ApprovalRequestStatus.New;

            switch (Status)
            {
                case "New":
                    x = ApprovalRequestStatus.New;
                    break;
                case "Approved":
                    x = ApprovalRequestStatus.Approved;
                    break;
                case "Rejected":
                    x = ApprovalRequestStatus.Rejected;
                    break;
                case "Canceled":
                    x = ApprovalRequestStatus.Canceled;
                    break;
                case "":
                case null:
                    break;
                default:
                    viewapprovalRequests = new List<ViewApprovalRequest> { };
                    break;



            }

            if (!string.IsNullOrEmpty(Status))
            {
                viewapprovalRequests = viewapprovalRequests.Where(y => y.Status == x).ToList();
            }
            if (!string.IsNullOrEmpty(Comment))
            {
                viewapprovalRequests = viewapprovalRequests.Where(x => x.Comment == Comment).ToList();
            }
            FilteredApprovalRequests = new ObservableCollection<ViewApprovalRequest>(viewapprovalRequests);
        }
        private void OnRowDoubleClick(ViewApprovalRequest item)
        {
            if (item != null)
            {
                if (AuthenticationHelper.loggedUser == null)
                {
                    _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _windowService.CloseWindow<ApprovalRequestsViewModel>();
                    return;
                }

                _windowService.ShowWindow<OpenApprovalRequestViewModel>(item.Id);
            }
        }
    }
}
