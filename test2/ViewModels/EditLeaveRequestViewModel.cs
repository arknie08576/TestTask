using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using test2.Helpers;
using Microsoft.EntityFrameworkCore;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;


namespace test2.ViewModels
{
    public class EditLeaveRequestViewModel : ViewModelBase, IParameterReceiver
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public string user;
        int id;
        public ICommand UpdateCommand { get; }
        public ICommand CancelCommand { get; }
        public EditLeaveRequestViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;

            _windowService = windowService;
            Items = new ObservableCollection<string> { "A", "B", "C", "D" };
            Items2 = new ObservableCollection<string> { "New", "Approved", "Rejected", "Canceled" };
            // Initialize commands
            UpdateCommand = new AsyncRelayCommand<object>(OnUpdateAsync);
            CancelCommand = new RelayCommand<object>(OnCancel);
            IsComboBoxEditable2 = false;
            IsComboBoxEditable = false;
            IsComboBoxEnabled2 = false;

        }
        private bool _isComboBoxEditable;
        public bool IsComboBoxEditable
        {
            get { return _isComboBoxEditable; }
            set
            {
                if (_isComboBoxEditable != value)
                {
                    _isComboBoxEditable = value;
                    OnPropertyChanged(nameof(IsComboBoxEditable));
                }
            }
        }
        private bool _isComboBoxEditable2;
        public bool IsComboBoxEditable2
        {
            get { return _isComboBoxEditable2; }
            set
            {
                if (_isComboBoxEditable2 != value)
                {
                    _isComboBoxEditable2 = value;
                    OnPropertyChanged(nameof(IsComboBoxEditable2));
                }
            }
        }
        private bool _isComboBoxEnabled = true;

        public bool IsComboBoxEnabled
        {
            get => _isComboBoxEnabled;
            set
            {
                if (_isComboBoxEnabled != value)
                {
                    _isComboBoxEnabled = value;
                    OnPropertyChanged(nameof(IsComboBoxEnabled));
                }
            }
        }

        private bool _isComboBoxEnabled2 = true;

        public bool IsComboBoxEnabled2
        {
            get => _isComboBoxEnabled2;
            set
            {
                if (_isComboBoxEnabled2 != value)
                {
                    _isComboBoxEnabled2 = value;
                    OnPropertyChanged(nameof(IsComboBoxEnabled2));
                }
            }
        }
        private bool _isTextBoxReadOnly;

        public bool IsTextBoxReadOnly
        {
            get { return _isTextBoxReadOnly; }
            set
            {
                if (_isTextBoxReadOnly != value)
                {
                    _isTextBoxReadOnly = value;
                    OnPropertyChanged(nameof(IsTextBoxReadOnly));
                }
            }
        }

        private bool _isButtonVisible;

        public bool IsButtonVisible
        {
            get { return _isButtonVisible; }
            set
            {
                if (_isButtonVisible != value)
                {
                    _isButtonVisible = value;
                    OnPropertyChanged(nameof(IsButtonVisible));
                }
            }
        }
        private bool _isButtonVisible2;

        public bool IsButtonVisible2
        {
            get { return _isButtonVisible2; }
            set
            {
                if (_isButtonVisible2 != value)
                {
                    _isButtonVisible2 = value;
                    OnPropertyChanged(nameof(IsButtonVisible2));
                }
            }
        }
        private ObservableCollection<string> _items;
        private string _selectedItem;

        public ObservableCollection<string> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
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

        public string SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        private ObservableCollection<string> _items2;
        private string _selectedItem2;

        public ObservableCollection<string> Items2
        {
            get => _items2;
            set => SetProperty(ref _items2, value);
        }

        public string SelectedItem2
        {
            get => _selectedItem2;
            set => SetProperty(ref _selectedItem2, value);

        }
        private bool _isStartDatePickerEnabled;
        public bool IsStartDatePickerEnabled
        {
            get { return _isStartDatePickerEnabled; }
            set
            {
                if (_isStartDatePickerEnabled != value)
                {
                    _isStartDatePickerEnabled = value;
                    OnPropertyChanged(nameof(IsStartDatePickerEnabled));
                }
            }
        }
        private bool _isEndDatePickerEnabled;
        public bool IsEndDatePickerEnabled
        {
            get { return _isEndDatePickerEnabled; }
            set
            {
                if (_isEndDatePickerEnabled != value)
                {
                    _isEndDatePickerEnabled = value;
                    OnPropertyChanged(nameof(IsEndDatePickerEnabled));
                }
            }
        }
        private bool _isStartDatePickerReadOnly;

        public bool IsStartDatePickerReadOnly
        {
            get => _isStartDatePickerReadOnly;
            set
            {
                if (_isStartDatePickerReadOnly != value)
                {
                    _isStartDatePickerReadOnly = value;
                    OnPropertyChanged(nameof(IsStartDatePickerReadOnly));
                }
            }
        }
        private bool _isEndDatePickerReadOnly;

        public bool IsEndDatePickerReadOnly
        {
            get => _isEndDatePickerReadOnly;
            set
            {
                if (_isEndDatePickerReadOnly != value)
                {
                    _isEndDatePickerReadOnly = value;
                    OnPropertyChanged(nameof(IsEndDatePickerReadOnly));
                }
            }
        }
        private DateTime? _startDate;

        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        private DateTime? _endDate;

        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }
        private async Task LoadLeaveRequestAsync()
        {
            Id = id.ToString();
            var status = await context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefaultAsync();
            var position = await context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefaultAsync();
            var lrOwner = await context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Employee).FirstOrDefaultAsync();
            var userId = await context.Employes.Where(e => e.Username == user).Select(x => x.Id).FirstOrDefaultAsync();
            if((status == LeaveRequestStatus.New && lrOwner == userId)|| (status == LeaveRequestStatus.New && position ==Position.Administrator))
            {
                IsComboBoxEnabled = true;
                IsStartDatePickerReadOnly = true;
                IsEndDatePickerReadOnly = true;
                IsTextBoxReadOnly = false;
                IsButtonVisible = true;
                IsButtonVisible2=true;

            }
            else
            {
                IsComboBoxEnabled = false;
                IsStartDatePickerReadOnly = false;
                IsEndDatePickerReadOnly = false;
                IsTextBoxReadOnly = true;
                IsButtonVisible = false;
                IsButtonVisible2 = false;
            }


            /*
            if ((status != LeaveRequestStatus.New || (position == Position.HRManager || position == Position.ProjectManager)))
            {
                IsTextBoxReadOnly = true;
                IsButtonVisible = false;
                IsComboBoxEditable = false;
            }
            else
            {
                IsButtonVisible = true;
                IsTextBoxReadOnly = false;
            } */

            //var products = await context.Employes.Where(e => e.Username == user).Select(x => x.FullName).ToListAsync();
            Id = id.ToString();
            var employeId = await context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Employee).FirstOrDefaultAsync();
            Employee = await context.Employes.Where(e => e.Id == employeId).Select(x => x.FullName).FirstOrDefaultAsync();
            AbsenceReason k = await context.LeaveRequests.Where(e => e.Id == id).Select(x => x.AbsenceReason).FirstOrDefaultAsync();
            switch (k)
            {
                case AbsenceReason.A:
                    SelectedItem = Items[0];
                    break;
                case AbsenceReason.B:
                    SelectedItem = Items[1];
                    break;
                case AbsenceReason.C:
                    SelectedItem = Items[2];
                    break;
                case AbsenceReason.D:
                    SelectedItem = Items[3];
                    break;
            }

            var start = await context.LeaveRequests.Where(e => e.Id == id).Select(x => x.StartDate).FirstOrDefaultAsync();
            StartDate = start.ToDateTime(TimeOnly.Parse("10:00 PM"));
            var end = await context.LeaveRequests.Where(e => e.Id == id).Select(x => x.EndDate).FirstOrDefaultAsync();
            EndDate = end.ToDateTime(TimeOnly.Parse("10:00 PM"));
            Comment = await context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Comment).FirstOrDefaultAsync();
            LeaveRequestStatus a = await context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefaultAsync();
            switch (a)
            {
                case LeaveRequestStatus.New:
                    SelectedItem2 = Items2[0];
                    break;
                case LeaveRequestStatus.Approved:
                    SelectedItem2 = Items2[1];
                    break;
                case LeaveRequestStatus.Rejected:
                    SelectedItem2 = Items2[2];
                    break;
                case LeaveRequestStatus.Canceled:
                    SelectedItem2 = Items2[3];
                    break;
            }

            if (status != LeaveRequestStatus.New || (position == Position.HRManager || position == Position.ProjectManager))
            {
                IsStartDatePickerEnabled = false;
                IsEndDatePickerEnabled = false;
            }
            else
            {
                IsStartDatePickerEnabled = true;
                IsEndDatePickerEnabled = true;
            }
        }
        public async Task ReceiveParameterAsync(object parameter)
        {
            if (parameter is int data)
            {
                id = data;

                await LoadLeaveRequestAsync();
            }
        }
        private async Task OnUpdateAsync(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {

                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<EditLeaveRequestViewModel>();
                return;
            }
            if (SelectedItem == "" || SelectedItem == null || !StartDate.HasValue || !EndDate.HasValue || SelectedItem2 == "" || SelectedItem2 == null)
            {
                _dialogService.ShowMessage("Fill in all required fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);


                return;


            }
            var employeeId = await context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefaultAsync();
            var leaveRequests = await context.LeaveRequests.Where(x => x.Employee == employeeId).ToListAsync();
            leaveRequests = leaveRequests.Where(x => (x.Status == LeaveRequestStatus.New || x.Status == LeaveRequestStatus.Approved)).OrderBy(x => x.StartDate).ToList();
            leaveRequests = leaveRequests.Where(x => x.Id != id).ToList();
            bool isCandidateOverlaping = false;

            foreach (var leaveRequest in leaveRequests)
            {

                if (DateOnly.FromDateTime(StartDate.Value) > leaveRequest.EndDate || DateOnly.FromDateTime(EndDate.Value) < leaveRequest.StartDate)
                {

                }
                else
                {
                    isCandidateOverlaping = true;
                }
            }

            bool isSpace = false;
            if (leaveRequests.Count > 0)
            {
                if (leaveRequests[0].StartDate > DateOnly.FromDateTime(EndDate.Value) || leaveRequests[leaveRequests.Count - 1].EndDate < DateOnly.FromDateTime(StartDate.Value))
                {
                    isSpace = true;

                }
            }
            else
            {
                isSpace = true;
            }
            for (int i = 0; i < leaveRequests.Count - 2; i++)
            {
                if (leaveRequests[i].EndDate < DateOnly.FromDateTime(StartDate.Value) && leaveRequests[i + 1].StartDate > DateOnly.FromDateTime(EndDate.Value))
                {
                    isSpace = true;
                }


            }
            if (!isSpace || isCandidateOverlaping)
            {

                _dialogService.ShowMessage("A specific date range covers a different New or Approved Leave Request.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;


            }


            if (StartDate.HasValue && EndDate.HasValue)
            {
                if (StartDate.Value > EndDate.Value)
                {


                    _dialogService.ShowMessage("Start date must be earlier than end date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            var obj = await context.LeaveRequests.FindAsync(id);
            switch (SelectedItem)
            {
                case "A":
                    obj.AbsenceReason = AbsenceReason.A;
                    break;
                case "B":
                    obj.AbsenceReason = AbsenceReason.B;
                    break;
                case "C":
                    obj.AbsenceReason = AbsenceReason.C;
                    break;
                case "D":
                    obj.AbsenceReason = AbsenceReason.D;
                    break;
            }

            obj.StartDate = DateOnly.FromDateTime(StartDate.Value);
            obj.EndDate = DateOnly.FromDateTime(EndDate.Value);
            obj.Comment = Comment;
            switch (SelectedItem2)
            {
                case "New":
                    obj.Status = LeaveRequestStatus.New;
                    break;
                case "Approved":
                    obj.Status = LeaveRequestStatus.Approved;
                    break;
                case "Rejected":
                    obj.Status = LeaveRequestStatus.Rejected;
                    break;
                case "Canceled":
                    obj.Status = LeaveRequestStatus.Canceled;
                    break;
            }

            context.Entry(obj).State = EntityState.Modified;
            await context.SaveChangesAsync();

            _dialogService.ShowMessage("Leave request updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<EditLeaveRequestViewModel>();
        }
        private async void OnCancel(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<EditLeaveRequestViewModel>();
                return;
            }

            var obj = await context.LeaveRequests.FindAsync(id);
            if (obj.Status == LeaveRequestStatus.Approved)
            {
                var emp = await context.Employes.FindAsync(obj.Employee);
                emp.Out_of_OfficeBalance += (obj.EndDate.ToDateTime(TimeOnly.MinValue) - obj.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1;
            }
            obj.Status = LeaveRequestStatus.Canceled;
            var ar = await context.ApprovalRequests.Where(x => x.LeaveRequest == obj.Id).FirstOrDefaultAsync();
            ar.Status = ApprovalRequestStatus.Canceled;
            context.Entry(obj).State = EntityState.Modified;
            await context.SaveChangesAsync();
            _dialogService.ShowMessage("Leave request canceled", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<EditLeaveRequestViewModel>();
        }
    }
}
