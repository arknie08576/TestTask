using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using test2.TableModels;
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;
using Microsoft.EntityFrameworkCore;

namespace test2.ViewModels
{
    public class LeaveRequestsViewModel : ViewModelBase
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        string user;
        public ICommand FilterCommand { get; }
        public ICommand NewLeaveRequestCommand { get; }
        public ICommand RowDoubleClickCommand { get; }
        private ObservableCollection<ViewLeaveRequest> _leaveRequests;
        public LeaveRequestsViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;

            _windowService = windowService;

            
            FilterCommand = new AsyncRelayCommand<object>(OnFilterAsync);
            NewLeaveRequestCommand = new RelayCommand<object>(OnNewLeaveRequest);
            RowDoubleClickCommand = new RelayCommand<ViewLeaveRequest>(OnRowDoubleClick);
            
            Task.Run(LoadLeaveRequestsAsync);
            
        }
        private async Task LoadLeaveRequestsAsync()
        {
            var leaveRequests = await context.LeaveRequests.ToListAsync();
            var position = await context.Employes.Where(x => x.Username == user).Select(x=>x.Position).FirstOrDefaultAsync();
            if (position == Position.Employee)
            {
                var employeId = await context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefaultAsync();
                leaveRequests = leaveRequests.Where(x => x.Employee == employeId).ToList();

            }
            var viewleaveRequests = new List<ViewLeaveRequest>();
            foreach (var leaveRequest in leaveRequests)
            {
                var fullName = await context.Employes.Where(x => x.Id == leaveRequest.Employee).Select(x => x.FullName).FirstOrDefaultAsync();
                ViewLeaveRequest p = new ViewLeaveRequest
                {
                    Id = leaveRequest.Id,
                    Employee = fullName,
                    AbsenceReasonn = leaveRequest.AbsenceReason,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    Comment = leaveRequest.Comment,
                    Status = leaveRequest.Status


                };
                viewleaveRequests.Add(p);


            }
            _leaveRequests = new ObservableCollection<ViewLeaveRequest>(viewleaveRequests);
            FilteredLeaveRequests = _leaveRequests;
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
        private string _employee;
        public string Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }
        private string _absenceReason;
        public string AbsenceReasonn
        {
            get => _absenceReason;
            set
            {
                _absenceReason = value;
                OnPropertyChanged(nameof(AbsenceReasonn));
            }
        }
        private string _startDate;
        public string StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private string _endDate;
        public string EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
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
        private ObservableCollection<ViewLeaveRequest> _filteredLeaveRequests;
        public ObservableCollection<ViewLeaveRequest> FilteredLeaveRequests
        {
            get { return _filteredLeaveRequests; }
            set
            {
                if (_filteredLeaveRequests != value)
                {
                    _filteredLeaveRequests = value;
                    OnPropertyChanged(nameof(FilteredLeaveRequests));
                }
            }
        }

        private async Task OnFilterAsync(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.CloseWindow<LeaveRequestsViewModel>();
                return;
            }
            var leaveRequests = await context.LeaveRequests.ToListAsync();
            var position = await context.Employes.Where(x => x.Username == user).Select(x => x.Position).FirstOrDefaultAsync();
            if (position == Position.Employee)
            {
                var employeeId = await context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefaultAsync();
                leaveRequests = leaveRequests.Where(x => x.Employee == employeeId).ToList();

            }
            var viewleaveRequests = new List<ViewLeaveRequest>();
            foreach (var leaveRequest in leaveRequests)
            {
                var fullName = await context.Employes.Where(x => x.Id == leaveRequest.Employee).Select(x => x.FullName).FirstOrDefaultAsync();
                ViewLeaveRequest p = new ViewLeaveRequest
                {
                    Id = leaveRequest.Id,
                    Employee = fullName,
                    AbsenceReasonn = leaveRequest.AbsenceReason,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    Comment = leaveRequest.Comment,
                    Status = leaveRequest.Status


                };
                viewleaveRequests.Add(p);


            }
            if (!string.IsNullOrEmpty(Id))
            {

                viewleaveRequests = viewleaveRequests.Where(x => x.Id.ToString() == Id).ToList();
            }

            if (!string.IsNullOrEmpty(Employee))
            {

                viewleaveRequests = viewleaveRequests.Where(x => x.Employee == _employee).ToList();
            }
            AbsenceReason x = AbsenceReason.A;

            switch (AbsenceReasonn)
            {
                case "A":
                    x = AbsenceReason.A;
                    break;
                case "B":
                    x = AbsenceReason.B;
                    break;
                case "C":
                    x = AbsenceReason.C;
                    break;
                case "D":
                    x = AbsenceReason.D;
                    break;
                case "":
                case null:
                    break;
                default:
                    viewleaveRequests = new List<ViewLeaveRequest> { };
                    break;



            }

            if (!string.IsNullOrEmpty(AbsenceReasonn))
            {

                viewleaveRequests = viewleaveRequests.Where(y => y.AbsenceReasonn == x).ToList();
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                string dateString = StartDate;
                string format = "dd/MM/yyyy";
                if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                {
                    viewleaveRequests = viewleaveRequests.Where(y => y.StartDate == date).ToList();
                }

            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                string dateString = EndDate;
                string format = "dd/MM/yyyy";
                if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                {
                    viewleaveRequests = viewleaveRequests.Where(y => y.EndDate == date).ToList();
                }

            }
            if (!string.IsNullOrEmpty(Comment))
            {

                viewleaveRequests = viewleaveRequests.Where(x => x.Comment == Comment).ToList();
            }
            LeaveRequestStatus f = LeaveRequestStatus.New;
            switch (Status)
            {
                case "New":
                    f = LeaveRequestStatus.New;
                    break;
                case "Approved":
                    f = LeaveRequestStatus.Approved;
                    break;
                case "Rejected":
                    f = LeaveRequestStatus.Rejected;
                    break;
                case "Canceled":
                    f = LeaveRequestStatus.Canceled;
                    break;
                case "":
                case null:
                    break;
                default:
                    viewleaveRequests = new List<ViewLeaveRequest> { };
                    break;



            }
            if (!string.IsNullOrEmpty(Status))
            {

                viewleaveRequests = viewleaveRequests.Where(y => y.Status == f).ToList();
            }

            _leaveRequests = new ObservableCollection<ViewLeaveRequest>(viewleaveRequests);
            FilteredLeaveRequests = _leaveRequests;
        }
        private void OnRowDoubleClick(ViewLeaveRequest item)
        {
            if (item != null)
            {
                if (AuthenticationHelper.loggedUser == null)
                {
                    _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _windowService.CloseWindow<LeaveRequestsViewModel>();
                    return;
                }             
                _dialogService.ShowMessage($"Double-clicked on: {item.Id}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.ShowWindow<EditLeaveRequestViewModel>(item.Id);
            }
        }
        private void OnNewLeaveRequest(object parameter)
        {
            _windowService.ShowWindow<NewLeaveRequestViewModel>();
        }
    }
}
