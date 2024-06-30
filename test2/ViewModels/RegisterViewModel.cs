using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.ComponentModel;
using test2.Models;
using System.Windows.Documents;
using System.Security;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows;
using test2.Views;
using test2.Helpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;

namespace test2.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        
        public RegisterViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
           
            ItemsSubdivision = new ObservableCollection<string> { "A", "B", "C", "D", "E", "F" };
            ItemsPosition = new ObservableCollection<string> { "Employee", "HRManager", "ProjectManager", "Administrator" };
            ItemsStatus = new ObservableCollection<string> { "Active", "Inactive" };
            var products = context.Employes.Where(e => e.Position == Position.HRManager).Select(x => x.FullName).ToList();
            ItemsPP = new ObservableCollection<string>(products);
            _windowService = windowService;

            // Initialize commands
            AddPhotoCommand = new RelayCommand<object>(OnAddPhoto);
            RegisterCommand = new RelayCommand<object>(OnRegister);
            //CloseCommand = new RelayCommand<object>(Close);
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



        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        private void Close()
        {
            
        }
        // Properties for ComboBoxes
        public ObservableCollection<string> ItemsSubdivision { get; set; }
        public ObservableCollection<string> ItemsPosition { get; set; }
        public ObservableCollection<string> ItemsStatus { get; set; }
        public ObservableCollection<string> ItemsPP { get; set; }
        public ICommand CloseCommand { get; }

        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private string _selectedItem2;
        public string SelectedItem2
        {
            get => _selectedItem2;
            set
            {
                _selectedItem2 = value;
                OnPropertyChanged(nameof(SelectedItem2));
            }
        }

        private string _selectedItem3;
        public string SelectedItem3
        {
            get => _selectedItem3;
            set
            {
                _selectedItem3 = value;
                OnPropertyChanged(nameof(SelectedItem3));
            }
        }

        private string _selectedItem4;
        public string SelectedItem4
        {
            get => _selectedItem4;
            set
            {
                _selectedItem4 = value;
                OnPropertyChanged(nameof(SelectedItem4));
            }
        }

        // Property for Image
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

        // Commands
        public ICommand AddPhotoCommand { get; }
        public ICommand RegisterCommand { get; }



        private void OnAddPhoto(object parameter)
        {
            // Add photo logic
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

        private void OnRegister(object parameter)
        {
            string username = _username;
            string password = _password;
            string confirmPassword = _confirmPassword;
            string fullName = _fullName;
            Subdivision? subdivision;
            Position? position;
            EmployeeStatus? status;
            int? peoplePartner = null;
            int out_of_OfficeBalance;
            byte[] photo = _imageSource;
            switch (_selectedItem)
            {
                case "A":
                    subdivision = Subdivision.A;
                    break;
                case "B":
                    subdivision = Subdivision.B;
                    break;
                case "C":
                    subdivision = Subdivision.C;
                    break;
                case "D":
                    subdivision = Subdivision.D;
                    break;
                case "E":
                    subdivision = Subdivision.E;
                    break;
                case "F":
                    subdivision = Subdivision.F;
                    break;
                default:
                    subdivision = null;
                    break;



            }

            switch (_selectedItem2)
            {
                case "Employee":
                    position = Position.Employee;
                    break;
                case "HRManager":
                    position = Position.HRManager;
                    break;
                case "ProjectManager":
                    position = Position.ProjectManager;
                    break;
                case "Administrator":
                    position = Position.Administrator;
                    break;
                default:
                    position = null;
                    break;

            }

            switch (_selectedItem3)
            {
                case "Active":
                    status = EmployeeStatus.Active;
                    break;
                case "Inactive":
                    status = EmployeeStatus.Inactive;
                    break;
                default:
                    status = null;
                    break;


            }

            if (!string.IsNullOrEmpty(_selectedItem4) )
            {
                string x = _selectedItem4;


                var partner = context.Employes.Where(e => e.Position == Position.HRManager).Where(x => x.FullName == _selectedItem4).Select(x => x.Id).FirstOrDefault();

                peoplePartner = partner;





            }
            int defaultBalance = 26;
            out_of_OfficeBalance = defaultBalance;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                _dialogService.ShowMessage("Username and password are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password != confirmPassword)
            {
                _dialogService.ShowMessage("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(fullName) || subdivision==null || position==null || status==null)
            {
                _dialogService.ShowMessage("Not all requiered fields are completed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

                AuthenticationHelper.RegisterUser(username, password, fullName, subdivision, position, status, peoplePartner, out_of_OfficeBalance, photo);
                _dialogService.ShowMessage("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.CloseWindow<RegisterViewModel>();
            }
            catch (InvalidOperationException ex)
            {
                _dialogService.ShowMessage(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // Registration logic
            //  string passwordText = SecurePasswordBoxBehavior.SecureStringToString(Password);
            //System.Windows.MessageBox.Show("Registered Successfully!");
        }

        public event PropertyChangedEventHandler PropertyChanged;
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

        // private System.Collections.IEnumerable itemsStatus;

        // public System.Collections.IEnumerable ItemsStatus { get => itemsStatus; set => SetProperty(ref itemsStatus, value); }
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
