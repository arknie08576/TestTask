using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using test2.Models;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;

using test2.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace test2
{

    public partial class RegisterWindow : Window
    {

        private readonly OfficeContex context;
        private readonly IWindowService _windowService;
        private byte[] photoData;
        public RegisterWindow( )
        {

           // context = officeContex;
            
            InitializeComponent();
            //var windowService = serviceProvider.GetRequiredService<IWindowService>();
           // _windowService = windowService;
            //DataContext = viewModel;
           // LoadComboBox();
        }
        /*
        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                // Read the photo from the file
                photoData = File.ReadAllBytes(openFileDialog.FileName);

                // Display the photo in the Image control
                PhotoImage.Source = LoadImage(photoData);

                // Save the photo to the database
               // SavePhotoToDatabase(photoData);
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
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string fullName = FullnameTextBox.Text;
            Subdivision subdivision;
            Position position;
            EmployeeStatus status;
            int? peoplePartner = null;
            int out_of_OfficeBalance;
            byte[] photo = photoData;

            switch (comboBox.Text)
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
                    subdivision = Subdivision.A;
                    break;



            }

            switch (comboBox2.Text)
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
                    position = Position.Employee;
                    break;

            }

            switch (comboBox3.Text)
            {
                case "Active":
                    status = EmployeeStatus.Active;
                    break;
                case "Inactive":
                    status = EmployeeStatus.Inactive;
                    break;
                default:
                    status = EmployeeStatus.Inactive;
                    break;


            }

            if (comboBox4.Text != "")
            {
                string x = comboBox4.Text;


                var partner = context.Employes.Where(e => e.Position == Position.HRManager).Where(x => x.FullName == comboBox4.Text).Select(x => x.Id).ToList();

                peoplePartner = partner[0];





            }
            int defaultBalance = 26;
            out_of_OfficeBalance = defaultBalance;
           // byte[] photo = PhotoTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (UsernameTextBox.Text == "" || PasswordBox.Password == "" || ConfirmPasswordBox.Password == "" || FullnameTextBox.Text == "" || comboBox.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Not all requiered fields are completed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {

                AuthenticationHelper.RegisterUser(username, password, fullName, subdivision, position, status, peoplePartner, out_of_OfficeBalance, photo);
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string Text = $"Selected: {selectedItem.Content}";
            }
        }
        */





        /*
        private void LoadComboBox()
        {


            var products = context.Employes.Where(e => e.Position == Position.HRManager).Select(x => x.FullName).ToList();
            comboBox4.ItemsSource = products;

        }*/
    }
}
