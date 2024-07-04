using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using test2.Models;
using System.Windows;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;

namespace test2.ViewModels
{
    public class OpenEmployeeViewModel : ViewModelBase, IParameterReceiver
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public string user;
        int id;
        public OpenEmployeeViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;

            _windowService = windowService;
            Items = new ObservableCollection<string> { "A", "B", "C", "D", "E", "F" };
            Items2 = new ObservableCollection<string> { "Employee", "HRManager", "ProjectManager", "Administrator" };
            Items3 = new ObservableCollection<string> { "Inactive", "Active" };


            AddPhotoCommand = new RelayCommand<object>(OnAddPhoto);
            UpdateCommand = new AsyncRelayCommand<object>(OnUpdateAsync);
            DeleteCommand = new AsyncRelayCommand<object>(OnDeleteAsync);
            RemovePhotoCommand = new RelayCommand<object>(OnRemovePhoto);
            
        }
        private async Task LoadItemsAsync()
        {
            var products = await context.Employes.Where(e => e.Position == Position.HRManager).Select(x => x.FullName).ToListAsync();
            Items4 = new ObservableCollection<string>(products);
            var projects = await context.Projects.OrderBy(x=>x.Id).Select(x => x.Id.ToString()).ToListAsync();
            Items5 = new ObservableCollection<string>(projects);

        }
        public ICommand AddPhotoCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RemovePhotoCommand { get; }
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
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
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
        private string _selectedItem2;
        public string SelectedItem2
        {
            get => _selectedItem2;
            set => SetProperty(ref _selectedItem2, value);
        }
        private ObservableCollection<string> _items2;
        public ObservableCollection<string> Items2
        {
            get => _items2;
            set => SetProperty(ref _items2, value);
        }
        private string _selectedItem3;
        public string SelectedItem3
        {
            get => _selectedItem3;
            set => SetProperty(ref _selectedItem3, value);
        }
        private ObservableCollection<string> _items3;
        public ObservableCollection<string> Items3
        {
            get => _items3;
            set => SetProperty(ref _items3, value);
        }
        private string _selectedItem4;
        public string SelectedItem4
        {
            get => _selectedItem4;
            set => SetProperty(ref _selectedItem4, value);
        }
        private ObservableCollection<string> _items4;
        public ObservableCollection<string> Items4
        {
            get => _items4;
            set => SetProperty(ref _items4, value);
        }
        private string _selectedItem5;
        public string SelectedItem5
        {
            get => _selectedItem5;
            set => SetProperty(ref _selectedItem5, value);
        }
        private ObservableCollection<string> _items5;
        public ObservableCollection<string> Items5
        {
            get => _items5;
            set => SetProperty(ref _items5, value);
        }
        private string _outofOfficeBalance;
        public string OutofOfficeBalance
        {
            get => _outofOfficeBalance;
            set
            {
                _outofOfficeBalance = value;
                OnPropertyChanged(nameof(OutofOfficeBalance));
            }
        }
        private byte[] _imageSource;
        public byte[] ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }
        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }
        private bool _isButtonVisible;

        public bool IsButtonVisible
        {
            get => _isButtonVisible;
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
            get => _isButtonVisible2;
            set
            {
                if (_isButtonVisible2 != value)
                {
                    _isButtonVisible2 = value;
                    OnPropertyChanged(nameof(IsButtonVisible2));
                }
            }
        }
        private bool _isButtonVisible3;

        public bool IsButtonVisible3
        {
            get => _isButtonVisible3;
            set
            {
                if (_isButtonVisible3 != value)
                {
                    _isButtonVisible3 = value;
                    OnPropertyChanged(nameof(IsButtonVisible3));
                }
            }
        }
        private async Task LoadOpenEmployeeAsync()
        {
            var ob = await context.Employes.Where(x => x.Username == user).FirstOrDefaultAsync();

            if (ob.Position == Position.ProjectManager)
            {

                IsButtonVisible = false;
                IsButtonVisible2=false;
                IsButtonVisible3=false;
                IsEnabled = false;
                IsReadOnly=true;

            }
            else
            {
                IsButtonVisible = true;
                IsButtonVisible2 = true;
                IsButtonVisible3 = true;
                IsEnabled = true;
                IsReadOnly = false;
            }
            Employee obj = await context.Employes.Where(x => x.Id == id).FirstOrDefaultAsync();
            Id = obj.Id.ToString();
            Username = obj.Username;

            FullName = obj.FullName;
            Subdivision k = obj.Subdivision;
            switch (k)
            {
                case Subdivision.A:

                    SelectedItem = Items[0];
                    break;
                case Subdivision.B:
                    SelectedItem = Items[1];
                    break;
                case Subdivision.C:
                    SelectedItem = Items[2];
                    break;
                case Subdivision.D:
                    SelectedItem = Items[3];
                    break;
                case Subdivision.E:
                    SelectedItem = Items[4];
                    break;
                case Subdivision.F:
                    SelectedItem = Items[5];
                    break;
            }

            Position c = obj.Position;
            switch (c)
            {
                case Position.Employee:
                    SelectedItem2 = Items2[0];
                    break;
                case Position.HRManager:
                    SelectedItem2 = Items2[1];
                    break;
                case Position.ProjectManager:
                    SelectedItem2 = Items2[2];
                    break;
                case Position.Administrator:
                    SelectedItem2 = Items2[3];
                    break;

            }
            EmployeeStatus d = obj.Status;
            switch (d)
            {
                case EmployeeStatus.Inactive:
                    SelectedItem3 = Items3[0];
                    break;
                case EmployeeStatus.Active:
                    SelectedItem3 = Items3[1];
                    break;


            }

            var products = await context.Employes.Where(e => e.Position == Position.HRManager).Select(x => x.FullName).ToListAsync();

            var t = obj.PeoplePartner;

            for (int i = 0; i < products.Count; i++)
            {
                var fullName = await context.Employes.Where(e => e.Id == t).Select(x => x.FullName).FirstOrDefaultAsync();
                if (products[i] == fullName)
                {

                    SelectedItem4 = Items4[i];
                }


            }
            OutofOfficeBalance = obj.Out_of_OfficeBalance.ToString();

            if (obj.Photo != null)
            {
                ImageSource = obj.Photo;
            }
            var product = await context.Projects.Select(x => x.Id).ToListAsync();

            var a = obj.AssignedProject;

            for (int i = 0; i < product.Count; i++)
            {
                var prj = await context.Projects.Where(e => e.Id == a).Select(x => x.Id).FirstOrDefaultAsync();
                if (product[i] == prj)
                {

                    SelectedItem5 = Items5[i];
                }


            }


        }
        private void OnAddPhoto(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                // Read the photo from the file
                var photoData = File.ReadAllBytes(openFileDialog.FileName);

                // Display the photo in the Image control
                ImageSource = photoData;

                // Save the photo to the database
                // SavePhotoToDatabase(photoData);
            }
        }
        private void OnRemovePhoto(object parameter)
        {
            ImageSource = null;
        }
        private async Task OnUpdateAsync(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<OpenEmployeeViewModel>();
                return;
            }

            if (Username.Length > 100)
            {
                _dialogService.ShowMessage("Username can't be longer than 100 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;


            }
            if (FullName.Length > 100)
            {
                _dialogService.ShowMessage("Full Name can't be longer than 100 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;


            }

            Employee obj = await context.Employes.Where(x => x.Id == id).FirstOrDefaultAsync();



            obj.Username = Username;

            obj.FullName = FullName;

            switch (SelectedItem)
            {
                case "A":
                    obj.Subdivision = Subdivision.A;
                    break;
                case "B":
                    obj.Subdivision = Subdivision.B;
                    break;
                case "C":
                    obj.Subdivision = Subdivision.C;
                    break;
                case "D":
                    obj.Subdivision = Subdivision.D;
                    break;
                case "E":
                    obj.Subdivision = Subdivision.E;
                    break;
                case "F":
                    obj.Subdivision = Subdivision.F;
                    break;

            }


            switch (SelectedItem2)
            {
                case "Employee":
                    obj.Position = Position.Employee;

                    break;
                case "HRManager":
                    obj.Position = Position.HRManager;
                    break;
                case "ProjectManager":
                    obj.Position = Position.ProjectManager;
                    break;
                case "Administrator":
                    obj.Position = Position.Administrator;
                    break;

            }
            switch (SelectedItem3)
            {
                case "Inactive":
                    obj.Status = EmployeeStatus.Inactive;
                    break;
                case "Active":
                    obj.Status = EmployeeStatus.Active;
                    break;


            }
            var products = await context.Employes.Where(e => e.Position == Position.HRManager).Select(x => x.FullName).ToListAsync();
            if (!string.IsNullOrEmpty(SelectedItem4))
            {
                obj.PeoplePartner = await context.Employes.Where(e => e.FullName == SelectedItem4).Select(x => x.Id).FirstOrDefaultAsync();
            }


            if (!string.IsNullOrEmpty(SelectedItem5))
            {
                obj.AssignedProject = await context.Projects.Where(e => e.Id.ToString() == SelectedItem5).Select(x => x.Id).FirstOrDefaultAsync();
            }
           
            
            if (int.TryParse(OutofOfficeBalance, out int result))
            {
                if (result < 0)
                {
                    _dialogService.ShowMessage("Out of Office Balance can't be less than zero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }


                obj.Out_of_OfficeBalance = result;
            }
            else
            {
                _dialogService.ShowMessage("Out of Office Balance is not number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            
            }

            obj.Photo = ImageSource;


            context.Entry(obj).State = EntityState.Modified;
            await context.SaveChangesAsync();

            _dialogService.ShowMessage("Employee updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<OpenEmployeeViewModel>();

        }
        private async Task OnDeleteAsync(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<OpenEmployeeViewModel>();
                return;

            }

            Employee obj = await context.Employes.Where(x => x.Id == id).FirstOrDefaultAsync();
            var leaveRequests = await context.LeaveRequests.Where(x => x.Employee == obj.Id).ToListAsync();
            var approvalRequests = new List<ApprovalRequest>();
            foreach (var lr in leaveRequests)
            {
                var ars = await context.ApprovalRequests.Where(x => x.LeaveRequest == lr.Id).ToListAsync();
                approvalRequests.AddRange(ars);


            }

            foreach (var ar in approvalRequests)
            {

                context.ApprovalRequests.Remove(ar);
            }

            foreach (var lr in leaveRequests)
            {
                context.LeaveRequests.Remove(lr);
            }
            if (obj.Position == Position.HRManager)
            {
                var employes = new List<Employee>();

                employes = await context.Employes.Where(x => x.PeoplePartner == obj.Id).ToListAsync();
                foreach (var emp in employes)
                {
                    emp.PeoplePartner = null;
                }



            }
            if (obj.Position == Position.ProjectManager)
            {
                var projects = new List<Project>();

                projects = await context.Projects.Where(x => x.ProjectManager == obj.Id).ToListAsync();
                foreach (var p in projects)
                {
                    p.ProjectManager = null;
                }
            }

            context.Employes.Remove(obj);
            await context.SaveChangesAsync();
            _dialogService.ShowMessage("Employee deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<OpenEmployeeViewModel>();


        }
        public async Task ReceiveParameterAsync(object parameter)
        {
            if (parameter is int data)
            {
                id = data;
                await LoadItemsAsync();
                await LoadOpenEmployeeAsync();
            }
        }
        private BitmapImage LoadImage(byte[] imageData)
        {
            BitmapImage image = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                ms.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = ms;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
