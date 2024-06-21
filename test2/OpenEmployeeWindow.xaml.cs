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

namespace test2
{

    public partial class OpenEmployeeWindow : Window
    {
        public int id;
        public string user;
        private readonly OfficeContex context;
        public OpenEmployeeWindow(int Id, OfficeContex officeContex)
        {
            this.id = Id;
            this.user = AuthenticationHelper.loggedUser;
            context = officeContex;
            InitializeComponent();

            var obj = context.Employes.Where(x => x.Username == user).FirstOrDefault();
            if (obj.Position == Position.HRManager)
            {
                //  AssignButton.Visibility=Visibility.Collapsed;


            }
            if (obj.Position == Position.ProjectManager)
            {
                //  UpdateButton.Visibility = Visibility.Collapsed;
                //  DeleteButton.Visibility = Visibility.Collapsed;

            }




            LoadEmployee();
        }
        private void LoadEmployee()
        {

            var ob = context.Employes.Where(x => x.Username == user).FirstOrDefault();
            if (ob.Position == Position.HRManager)
            {



            }
            if (ob.Position == Position.ProjectManager)
            {
                //UpdateButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;

            }
            Employee obj = context.Employes.Where(x => x.Id == id).FirstOrDefault();
            IdTextBox.Text = obj.Id.ToString();
            UsernameTextBox.Text = obj.Username;
            PasswordBox.Text = obj.PasswordHash;
            SaltBox.Text = obj.Salt;
            FullnameTextBox.Text = obj.FullName;
            Subdivision k = obj.Subdivision;
            switch (k)
            {
                case Subdivision.A:
                    comboBox.SelectedIndex = 0;
                    break;
                case Subdivision.B:
                    comboBox.SelectedIndex = 1;
                    break;
                case Subdivision.C:
                    comboBox.SelectedIndex = 2;
                    break;
                case Subdivision.D:
                    comboBox.SelectedIndex = 3;
                    break;
                case Subdivision.E:
                    comboBox.SelectedIndex = 4;
                    break;
                case Subdivision.F:
                    comboBox.SelectedIndex = 5;
                    break;
            }

            Position c = obj.Position;
            switch (c)
            {
                case Position.Employee:
                    comboBox2.SelectedIndex = 0;
                    break;
                case Position.HRManager:
                    comboBox2.SelectedIndex = 1;
                    break;
                case Position.ProjectManager:
                    comboBox2.SelectedIndex = 2;
                    break;
                case Position.Administrator:
                    comboBox2.SelectedIndex = 3;
                    break;

            }
            EmployeeStatus d = obj.Status;
            switch (d)
            {
                case EmployeeStatus.Inactive:
                    comboBox3.SelectedIndex = 0;
                    break;
                case EmployeeStatus.Active:
                    comboBox3.SelectedIndex = 1;
                    break;


            }

            var products = context.Employes.Where(e => e.Position == Position.HRManager).Select(x => x.FullName).ToList();
            comboBox4.ItemsSource = products;
            var t = obj.PeoplePartner;

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i] == context.Employes.Where(e => e.Id == t).Select(x => x.FullName).FirstOrDefault())
                {
                    comboBox4.SelectedIndex = i;
                }


            }
            Out_of_OfficeBalanceTextBox.Text = obj.Out_of_OfficeBalance.ToString();
            PhotoTextBox.Text = obj.Photo;
            var product = context.Projects.Select(x => x.Id).ToList();
            comboBox5.ItemsSource = product;
            var a = obj.AssignedProject;

            for (int i = 0; i < product.Count; i++)
            {
                if (product[i] == context.Projects.Where(e => e.Id == a).Select(x => x.Id).FirstOrDefault())
                {
                    comboBox5.SelectedIndex = i;
                }


            }


        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string Text = $"Selected: {selectedItem.Content}";
            }
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {




            Employee obj = context.Employes.Where(x => x.Id == id).FirstOrDefault();



            obj.Username = UsernameTextBox.Text;
            obj.PasswordHash = PasswordBox.Text;
            obj.Salt = SaltBox.Text;
            obj.FullName = FullnameTextBox.Text;

            switch (comboBox.Text)
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


            switch (comboBox2.Text)
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
            switch (comboBox3.Text)
            {
                case "Inactive":
                    obj.Status = EmployeeStatus.Inactive;
                    break;
                case "Active":
                    obj.Status = EmployeeStatus.Active;
                    break;


            }
            var products = context.Employes.Where(e => e.Position == Position.HRManager).Select(x => x.FullName).ToList();
            if (comboBox4.Text != "")
            {
                obj.PeoplePartner = context.Employes.Where(e => e.FullName == comboBox4.Text).Select(x => x.Id).FirstOrDefault();
            }


            if (comboBox5.Text != "")
            {
                obj.AssignedProject = context.Projects.Where(e => e.Id.ToString() == comboBox5.Text).Select(x => x.Id).FirstOrDefault();
            }
            obj.Out_of_OfficeBalance = int.Parse(Out_of_OfficeBalanceTextBox.Text);
            obj.Photo = PhotoTextBox.Text;
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            MessageBox.Show("Employee updated.");
            this.Close();











        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            Employee obj = context.Employes.Where(x => x.Id == id).ToList()[0];
            var leaveRequests = context.LeaveRequests.Where(x => x.Employee == obj.Id).ToList();
            var approvalRequests = new List<ApprovalRequest>();
            foreach (var lr in leaveRequests)
            {
                approvalRequests.AddRange(context.ApprovalRequests.Where(x => x.LeaveRequest == lr.Id));


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

                employes = context.Employes.Where(x => x.PeoplePartner == obj.Id).ToList();
                foreach (var emp in employes)
                {
                    emp.PeoplePartner = null;
                }



            }
            if (obj.Position == Position.ProjectManager)
            {
                var projects = new List<Project>();

                projects = context.Projects.Where(x => x.ProjectManager == obj.Id).ToList();
                foreach (var p in projects)
                {
                    p.ProjectManager = null;
                }



            }



            context.Employes.Remove(obj);
            context.SaveChanges();
            MessageBox.Show("Employee deleted.");
            this.Close();

        }
    }
}
