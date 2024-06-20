using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using test2.ViewModels;

namespace test2
{
    /// <summary>
    /// Interaction logic for EmployesWindow.xaml
    /// </summary>
    public partial class EmployesWindow : Window
    {
        string user;
        private readonly OfficeContex context;
        public EmployesWindow(OfficeContex officeContex)
        {
            InitializeComponent();
            user = AuthenticationHelper.loggedUser;
            context = officeContex;
            LoadEmployes();
        }
        public void LoadEmployes()
        {


                var ob = context.Employes.Where(x => x.Username == user).FirstOrDefault();
                if (ob.Position == Position.HRManager)
                {
                    


                }
                if (ob.Position == Position.ProjectManager)
                {
                    NewButton.Visibility = Visibility.Collapsed;
                   

                }
                var employes = context.Employes.ToList();
                var viewemployes = new List<ViewEmployee>();
                foreach (var employe in employes)
                {

                    ViewEmployee p = new ViewEmployee
                    {
                        Id = employe.Id,
                        Username = employe.Username,
                        PasswordHash = employe.PasswordHash,
                        Salt = employe.Salt,
                        FullName = employe.FullName,
                        Subdivision = employe.Subdivision,
                        Position = employe.Position,
                        Status = employe.Status,
                        PeoplePartner = context.Employes.Where(x => x.Id == employe.PeoplePartner).Select(x => x.FullName).FirstOrDefault(),
                        Out_of_OfficeBalance = employe.Out_of_OfficeBalance,
                        Photo = employe.Photo

                    };
                    viewemployes.Add(p);


                }

                ProjectDataGrid.ItemsSource = viewemployes;

            
            // Example list of projects



        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {

                var employes = context.Employes.ToList();
                var viewemployes = new List<ViewEmployee>();
                foreach (var employe in employes)
                {

                    ViewEmployee p = new ViewEmployee
                    {
                        Id = employe.Id,
                        Username = employe.Username,
                        PasswordHash = employe.PasswordHash,
                        Salt = employe.Salt,
                        FullName = employe.FullName,
                        Subdivision = employe.Subdivision,
                        Position = employe.Position,
                        Status = employe.Status,
                        PeoplePartner = context.Employes.Where(x => x.Id == employe.PeoplePartner).Select(x => x.FullName).FirstOrDefault(),
                        Out_of_OfficeBalance = employe.Out_of_OfficeBalance,
                        Photo = employe.Photo

                    };
                    viewemployes.Add(p);


                }

                if (IdTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(y => y.Id.ToString() == IdTextBox.Text).ToList();
                }
                if (UsernameTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(y => y.Username == UsernameTextBox.Text).ToList();
                }

                if (PasswordHashTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(y => y.PasswordHash == PasswordHashTextBox.Text).ToList();
                }

                if (SaltTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(y => y.Salt == SaltTextBox.Text).ToList();
                }
                if (FullNameTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(y => y.FullName == FullNameTextBox.Text).ToList();
                }






                Subdivision x = Subdivision.A;

                switch (SubdivisionTextBox.Text)
                {
                    case "A":
                        x = Subdivision.A;
                        break;
                    case "B":
                        x = Subdivision.B;
                        break;
                    case "C":
                        x = Subdivision.C;
                        break;
                    case "D":
                        x = Subdivision.D;
                        break;
                    case "E":
                        x = Subdivision.E;
                        break;
                    case "F":
                        x = Subdivision.F;
                        break;
                    case "":
                        break;
                    default:
                        viewemployes = new List<ViewEmployee> { };
                        break;



                }

                if (SubdivisionTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(y => y.Subdivision == x).ToList();
                }


                Position y = Position.Employee;

                switch (PositionTextBox.Text)
                {
                    case "Employee":
                        y = Position.Employee;
                        break;
                    case "HRManager":
                        y = Position.HRManager;
                        break;
                    case "ProjectManager":
                        y = Position.ProjectManager;
                        break;
                    case "Administrator":
                        y = Position.Administrator;
                        break;
                    case "":
                        break;
                    default:
                        viewemployes = new List<ViewEmployee> { };
                        break;



                }

                if (SubdivisionTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(x => x.Position == y).ToList();
                }

                EmployeeStatus s = EmployeeStatus.Active;

                switch (StatusTextBox.Text)
                {
                    case "Active":
                        s = EmployeeStatus.Active;
                        break;
                    case "Inactive":
                        s = EmployeeStatus.Inactive;
                        break;
                    case "":
                        break;
                    default:
                        viewemployes = new List<ViewEmployee> { };
                        break;



                }

                if (StatusTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(x => x.Status == s).ToList();
                }

                if (PeoplePartnerTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(y => y.PeoplePartner == PeoplePartnerTextBox.Text).ToList();
                }


                if (OutOfOfficeBalanceTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(y => y.Out_of_OfficeBalance.ToString() == OutOfOfficeBalanceTextBox.Text).ToList();
                }

                if (PhotoTextBox.Text != "")
                {

                    viewemployes = viewemployes.Where(y => y.Photo == PhotoTextBox.Text).ToList();
                }

                ProjectDataGrid.ItemsSource = viewemployes;



            
        }

        private void NewEmployeeButton_Click(object sender, RoutedEventArgs e)
        {


        }
        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {


        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (ProjectDataGrid.SelectedItem is ViewEmployee selectedData)
            {

                MessageBox.Show($"You double-clicked on: {selectedData.Id}");

                OpenEmployeeWindow eWindow = new OpenEmployeeWindow(selectedData.Id, user);
                eWindow.Show();

            }


        }
    }
}

