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
using test2.Models;
using test2.TableModels;
using test2.View;

namespace test2.ViewModels
{
    public class LeaveRequestsViewModel : INotifyPropertyChanged
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
        string user;
        public ICommand FilterCommand { get; }
        public ICommand NewLeaveRequestCommand { get; }
        private ObservableCollection<ViewLeaveRequest> _leaveRequests;
        public LeaveRequestsViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;

            _windowService = windowService;

            // Initialize commands
            FilterCommand = new RelayCommand<object>(OnFilter);
            NewLeaveRequestCommand = new RelayCommand<object>(OnNewLeaveRequest);

            //CloseCommand = new RelayCommand<object>(Close);
            var leaveRequests = context.LeaveRequests.ToList();
            if (context.Employes.Where(x => x.Username == user).FirstOrDefault().Position == Position.Employee)
            {
                leaveRequests = leaveRequests.Where(x => x.Employee == context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefault()).ToList();

            }
            var viewleaveRequests = new List<ViewLeaveRequest>();
            foreach (var leaveRequest in leaveRequests)
            {

                ViewLeaveRequest p = new ViewLeaveRequest
                {
                    Id = leaveRequest.Id,
                    Employee = context.Employes.Where(x => x.Id == leaveRequest.Employee).Select(x => x.FullName).FirstOrDefault(),
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

        private void OnFilter(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.CloseWindow<LeaveRequestsViewModel>();
                return;
            }
            var leaveRequests = context.LeaveRequests.ToList();
            if (context.Employes.Where(x => x.Username == user).FirstOrDefault().Position == Position.Employee)
            {
                leaveRequests = leaveRequests.Where(x => x.Employee == context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefault()).ToList();

            }
            var viewleaveRequests = new List<ViewLeaveRequest>();
            foreach (var leaveRequest in leaveRequests)
            {

                ViewLeaveRequest p = new ViewLeaveRequest
                {
                    Id = leaveRequest.Id,
                    Employee = context.Employes.Where(x => x.Id == leaveRequest.Employee).Select(x => x.FullName).FirstOrDefault(),
                    AbsenceReasonn = leaveRequest.AbsenceReason,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    Comment = leaveRequest.Comment,
                    Status = leaveRequest.Status


                };
                viewleaveRequests.Add(p);


            }
            if (!string.IsNullOrEmpty(_id))
            {

                viewleaveRequests = viewleaveRequests.Where(x => x.Id == Convert.ToInt32(_id)).ToList();
            }

            if (!string.IsNullOrEmpty(_employee))
            {

                viewleaveRequests = viewleaveRequests.Where(x => x.Employee == _employee).ToList();
            }
            AbsenceReason x = AbsenceReason.A;

            switch (_absenceReason)
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

            if (!string.IsNullOrEmpty(_absenceReason))
            {

                viewleaveRequests = viewleaveRequests.Where(y => y.AbsenceReasonn == x).ToList();
            }
            if (!string.IsNullOrEmpty(_startDate))
            {
                string dateString = _startDate;
                string format = "dd/MM/yyyy";
                if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                {
                    viewleaveRequests = viewleaveRequests.Where(y => y.StartDate == date).ToList();
                }

            }
            if (!string.IsNullOrEmpty(_endDate))
            {
                string dateString = _endDate;
                string format = "dd/MM/yyyy";
                if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                {
                    viewleaveRequests = viewleaveRequests.Where(y => y.EndDate == date).ToList();
                }

            }
            if (!string.IsNullOrEmpty(_comment))
            {

                viewleaveRequests = viewleaveRequests.Where(x => x.Comment == _comment).ToList();
            }
            LeaveRequestStatus f = LeaveRequestStatus.New;
            switch (_status)
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
            if (!string.IsNullOrEmpty(_status))
            {

                viewleaveRequests = viewleaveRequests.Where(y => y.Status == f).ToList();
            }

            _leaveRequests = new ObservableCollection<ViewLeaveRequest>(viewleaveRequests);
            FilteredLeaveRequests = _leaveRequests;







        }
        private void OnNewLeaveRequest(object parameter)
        { }
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
