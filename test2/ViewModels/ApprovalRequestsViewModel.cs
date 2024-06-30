using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using test2.TableModels;
using test2.Views;
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;

namespace test2.ViewModels
{
    public class ApprovalRequestsViewModel : INotifyPropertyChanged
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
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

            // Initialize commands
            FilterCommand = new RelayCommand<object>(OnFilter);

            RowDoubleClickCommand = new RelayCommand<ViewApprovalRequest>(OnRowDoubleClick);
            //CloseCommand = new RelayCommand<object>(Close);
            LoadApprovalRequests();
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
        private void LoadApprovalRequests()
        {
            var approvalRequests = context.ApprovalRequests.ToList();
            var viewapprovalRequests = new List<ViewApprovalRequest>();
            foreach (var approvalRequest in approvalRequests)
            {

                ViewApprovalRequest p = new ViewApprovalRequest
                {
                    Id = approvalRequest.Id,
                    Approver = context.Employes.Where(x => x.Id == approvalRequest.Approver).Select(x => x.FullName).FirstOrDefault(),
                    LeaveRequestt = approvalRequest.LeaveRequest,
                    Status = approvalRequest.Status,
                    Comment = approvalRequest.Comment




                };
                viewapprovalRequests.Add(p);


            }

            FilteredApprovalRequests = new ObservableCollection<ViewApprovalRequest>(viewapprovalRequests);


        }
        private void OnFilter(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.CloseWindow<ApprovalRequestsViewModel>();
                return;
            }

            var approvalRequests = context.ApprovalRequests.ToList();
            var viewapprovalRequests = new List<ViewApprovalRequest>();
            foreach (var approvalRequest in approvalRequests)
            {

                ViewApprovalRequest p = new ViewApprovalRequest
                {
                    Id = approvalRequest.Id,
                    Approver = context.Employes.Where(x => x.Id == approvalRequest.Approver).Select(x => x.FullName).FirstOrDefault(),
                    LeaveRequestt = approvalRequest.LeaveRequest,
                    Status = approvalRequest.Status,
                    Comment = approvalRequest.Comment




                };
                viewapprovalRequests.Add(p);


            }



            if (!string.IsNullOrEmpty(Id))
            {

                viewapprovalRequests = viewapprovalRequests.Where(x => x.Id == Convert.ToInt32(Id)).ToList();
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
                // Perform your action here

                _dialogService.ShowMessage($"Double-clicked on: {item.Id}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.ShowWindow<OpenApprovalRequestViewModel>(item.Id);
            }


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
