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

namespace test2.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly OfficeContex context;
        private byte[] photoData;
        public RegisterViewModel(OfficeContex officeContex)
        {
            context = officeContex;
            ItemsSubdivision = new ObservableCollection<string> { "A", "B", "C", "D", "E", "F" };
            ItemsPosition = new ObservableCollection<string> { "Employee", "HRManager", "ProjectManager", "Administrator" };
            ItemsStatus = new ObservableCollection<string> { "Active", "Inactive" };
            var products = context.Employes.Where(e => e.Position == Position.HRManager).Select(x => x.FullName).ToList();
            ItemsPP = new ObservableCollection<string>(products);


            // Initialize commands
            AddPhotoCommand = new RelayCommand(OnAddPhoto);
            RegisterCommand = new RelayCommand(OnRegister);

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



        private SecureString _password;

        public SecureString Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private SecureString _confirmpassword;

        public SecureString ConfirmPassword
        {
            get { return _confirmpassword; }
            set { SetProperty(ref _confirmpassword, value); }
        }

        // Properties for ComboBoxes
        public ObservableCollection<string> ItemsSubdivision { get; set; }
        public ObservableCollection<string> ItemsPosition { get; set; }
        public ObservableCollection<string> ItemsStatus { get; set; }
        public ObservableCollection<string> ItemsPP { get; set; }

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
        private ImageSource _imageSource;
        public ImageSource ImageSource
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
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/sample.jpg"));
        }

        private void OnRegister(object parameter)
        {
            // Registration logic
            string passwordText = SecurePasswordBoxBehavior.SecureStringToString(Password);
            System.Windows.MessageBox.Show("Registered Successfully!");
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

    }
}
