using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test2.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using test2.Models;
using test2.Helpers;
using Microsoft.EntityFrameworkCore;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;

namespace test2.ViewModels
{
    public class EditLeaveRequestViewModel : INotifyPropertyChanged, IParameterReceiver
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
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
            UpdateCommand = new RelayCommand<object>(OnUpdate);
            CancelCommand = new RelayCommand<object>(OnCancel);
            IsComboBoxEditable2=false;
            IsComboBoxEditable = false;
            IsComboBoxEnabled2=false;

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
        private void LoadLeaveRequest()
        {
            Id = id.ToString();
            if ((context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager)))
            {
                IsTextBoxReadOnly = true;
                IsButtonVisible = false;
            }
            else
            {
                IsButtonVisible = true;
                IsTextBoxReadOnly = false;
            }
            if ((context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager)))
            {
                IsComboBoxEditable = false;
            }
            var products = context.Employes.Where(e => e.Username == user).Select(x => x.FullName).ToList();
            Id = id.ToString();
            Employee = context.Employes.Where(e => e.Username == user).Select(x => x.FullName).FirstOrDefault();
            AbsenceReason k = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.AbsenceReason).FirstOrDefault();
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

            StartDate = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.StartDate).FirstOrDefault().ToDateTime(TimeOnly.Parse("10:00 PM"));
            EndDate = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.EndDate).FirstOrDefault().ToDateTime(TimeOnly.Parse("10:00 PM"));
            Comment = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Comment).FirstOrDefault();
            LeaveRequestStatus a = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault();
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
            if (context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager))
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
        public void ReceiveParameter(object parameter)
        {
            if (parameter is int data)
            {
                id = data;
                
                LoadLeaveRequest();
            }
        }
        private void OnUpdate(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                
                _dialogService.ShowMessage("User logged out", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.CloseWindow<EditLeaveRequestViewModel>();
                return;
            }
            if (SelectedItem == "" || SelectedItem == null || !StartDate.HasValue || !EndDate.HasValue || SelectedItem2 == "" || SelectedItem2 == null)
            {
                _dialogService.ShowMessage("Fill in all required fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                

                return;


            }
            var leaveRequests = context.LeaveRequests.Where(x => x.Employee == context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefault()).Where(x => (x.Status == LeaveRequestStatus.New || x.Status == LeaveRequestStatus.Approved)).OrderBy(x => x.StartDate).ToList();
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

            var obj = context.LeaveRequests.Find(id);
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
            context.SaveChanges();
            
            _dialogService.ShowMessage("Leave request updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<EditLeaveRequestViewModel>();
        }
        private void OnCancel(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                
                _dialogService.ShowMessage("User logged out", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.CloseWindow<EditLeaveRequestViewModel>();
                return;
            }

            var obj = context.LeaveRequests.Find(id);
            if (obj.Status == LeaveRequestStatus.Approved)
            {
                var emp = context.Employes.Find(obj.Employee);
                emp.Out_of_OfficeBalance += (obj.EndDate.ToDateTime(TimeOnly.MinValue) - obj.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1;
            }
            obj.Status = LeaveRequestStatus.Canceled;
            var ar = context.ApprovalRequests.Where(x => x.LeaveRequest == obj.Id).FirstOrDefault();
            ar.Status = ApprovalRequestStatus.Canceled;
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            
            _dialogService.ShowMessage("Leave request canceled", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<EditLeaveRequestViewModel>();

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
