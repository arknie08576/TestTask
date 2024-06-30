using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Input;
using test2.Views;
using test2.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;

namespace test2.ViewModels
{
    public class OpenApprovalRequestViewModel : INotifyPropertyChanged, IParameterReceiver
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
        public string user;
        int id;
        public OpenApprovalRequestViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;

            _windowService = windowService;
            Items = new ObservableCollection<string> { "New", "Approved", "Rejected", "Canceled" };


            ApproveCommand = new RelayCommand<object>(OnApprove);
            RejectCommand = new RelayCommand<object>(OnReject);

        }
        public ICommand ApproveCommand { get; }
        public ICommand RejectCommand { get; }
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
        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        private ObservableCollection<string> _items;
        public ObservableCollection<string> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
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
        private void LoadOpenApprovalRequest()
        {
            Id = id.ToString();
            var t = context.ApprovalRequests.Where(e => e.Id == id).Select(x => x.Approver).FirstOrDefault();
            Approver = context.Employes.Where(e => e.Id == t).Select(x => x.FullName).FirstOrDefault();
            LeaveRequestt = context.ApprovalRequests.Where(e => e.Id == id).Select(x => x.LeaveRequest).FirstOrDefault().ToString();


            ApprovalRequestStatus k = context.ApprovalRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault();
            switch (k)
            {
                case ApprovalRequestStatus.New:
                    
                    SelectedItem = Items[0];
                    IsButtonVisible = true;
                    IsButtonVisible2=true;


                    break;
                case ApprovalRequestStatus.Approved:
                    SelectedItem = Items[1];
                    IsButtonVisible=false;
                    IsButtonVisible2 = false;
                    break;
                case ApprovalRequestStatus.Rejected:
                    SelectedItem = Items[2];
                    
                    IsButtonVisible = false;
                    
                    IsButtonVisible2 = false;
                    break;
                case ApprovalRequestStatus.Canceled:
                    SelectedItem = Items[3];
                    IsButtonVisible = false;
                    IsButtonVisible2 = false;
                    break;
            }
            IsComboBoxEnabled = false;
            Comment = context.ApprovalRequests.Where(e => e.Id == id).Select(x => x.Comment).FirstOrDefault();



        }
        private void OnApprove(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.CloseWindow<OpenApprovalRequestViewModel>();
                return;
            }
            var obj = context.ApprovalRequests.Find(id);
            obj.Approver = context.Employes.Where(e => e.Username == user).Select(x => x.Id).FirstOrDefault();
            obj.Status = ApprovalRequestStatus.Approved;
            obj.Comment = Comment;
            var lr = context.LeaveRequests.Where(e => e.Id == obj.LeaveRequest).FirstOrDefault();
            var emp = context.Employes.Where((e) => e.Id == lr.Employee).FirstOrDefault();
            lr.Status = LeaveRequestStatus.Approved;
            emp.Out_of_OfficeBalance -= (obj.leaveRequest.EndDate.ToDateTime(TimeOnly.MinValue) - obj.leaveRequest.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1;
            context.Entry(lr).State = EntityState.Modified;
            context.Entry(emp).State = EntityState.Modified;
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            
            _dialogService.ShowMessage("Approval request approved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<OpenApprovalRequestViewModel>();
        }
        private void OnReject(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.CloseWindow<OpenApprovalRequestViewModel>();
                return;
            }
            var obj = context.ApprovalRequests.Find(id);
            var lr = context.LeaveRequests.Where(e => e.Id == obj.LeaveRequest).FirstOrDefault();
            obj.Approver = context.Employes.Where(e => e.Username == user).Select(x => x.Id).FirstOrDefault();

            var emp = context.Employes.Where((e) => e.Id == lr.Employee).FirstOrDefault();
            if (obj.Status == ApprovalRequestStatus.Approved)
            {
                emp.Out_of_OfficeBalance += (obj.leaveRequest.EndDate.ToDateTime(TimeOnly.MinValue) - obj.leaveRequest.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1;

            }
            obj.Status = ApprovalRequestStatus.Rejected;
            obj.Comment = Comment;


            lr.Status = LeaveRequestStatus.Rejected;
            //obj.leaveRequest.Status = LeaveRequestStatus.Rejected;
            context.Entry(lr).State = EntityState.Modified;
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            _dialogService.ShowMessage("Approval request rejected", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<OpenApprovalRequestViewModel>();
        }
        public void ReceiveParameter(object parameter)
        {
            if (parameter is int data)
            {
                id = data;

                LoadOpenApprovalRequest();
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
