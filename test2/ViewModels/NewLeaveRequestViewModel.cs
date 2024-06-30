using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using test2.Models;
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;
using Microsoft.EntityFrameworkCore;


namespace test2.ViewModels
{
    public class NewLeaveRequestViewModel : ViewModelBase
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public string user;
        public NewLeaveRequestViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;
            _windowService = windowService;
            Items = new ObservableCollection<string> { "A", "B", "C", "D" };
            Items2 = new ObservableCollection<string> { "New", "Approved", "Rejected", "Canceled" };
            SelectedItem2 = Items2[0];
            SubmitCommand = new AsyncRelayCommand<object>(OnSubmitAsync);
            Task.Run(LoadEmployeeAsync);
        }
       private async Task LoadEmployeeAsync()
        {
            Employee = await context.Employes.Where(e => e.Username == user).Select(x => x.FullName).FirstOrDefaultAsync();

        }

        public ObservableCollection<string> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
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
        private ObservableCollection<string> _items;
        private string _selectedItem;
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
        public ICommand SubmitCommand { get; }
        private async Task OnSubmitAsync(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                //MessageBox.Show("User logged out");
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<NewLeaveRequestViewModel>();
                return;
            }
            if (!StartDate.HasValue || !EndDate.HasValue || string.IsNullOrEmpty(SelectedItem) )
            {
                
                _dialogService.ShowMessage("Fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (StartDate.HasValue && EndDate.HasValue)
            {
                if (StartDate > EndDate)
                {

                    
                    _dialogService.ShowMessage("Start date must be earlier than end date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            var emp = await context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefaultAsync();
            var leaveRequests = await context.LeaveRequests.Where(x => x.Employee == emp).Where(x => (x.Status == LeaveRequestStatus.New || x.Status == LeaveRequestStatus.Approved)).OrderBy(x => x.StartDate).ToListAsync();

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



            var obj = new LeaveRequest();
            obj.Employee = await context.Employes.Where(e => e.Username == user).Select(x => x.Id).FirstOrDefaultAsync();
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
            obj.Status = LeaveRequestStatus.New;
            await context.LeaveRequests.AddAsync(obj);
            await context.SaveChangesAsync();
            var ar = new ApprovalRequest();
            ar.LeaveRequest = obj.Id;
            ar.Status = ApprovalRequestStatus.New;
            await context.ApprovalRequests.AddAsync(ar);
            await context.SaveChangesAsync();  
            _dialogService.ShowMessage("Leave request added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<NewLeaveRequestViewModel>();
        }
    }
}
