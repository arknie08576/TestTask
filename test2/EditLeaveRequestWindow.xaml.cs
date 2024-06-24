using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using test2.Models;

namespace test2
{

    public partial class EditLeaveRequestWindow : Window
    {
        string user;
        int id;
        private readonly OfficeContex context;
        public EditLeaveRequestWindow(int Id, OfficeContex officeContex)
        {
            InitializeComponent();
            user = AuthenticationHelper.loggedUser;
            context = officeContex;
            id = Id;
            LoadLeaveRequest();

        }
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {

                var textBox = (TextBox)comboBox.Template.FindName("PART_EditableTextBox", comboBox);
                if (textBox != null && (context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager)))
                {
                    textBox.IsReadOnly = true;
                }
                else if (textBox != null)
                {
                    textBox.IsReadOnly = false;
                }
            }
        }
        private void StartDatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is DatePicker datePicker)
            {
                datePicker.IsEnabled = true;
                var textBox = (TextBox)datePicker.Template.FindName("PART_TextBox", datePicker);
                if (textBox != null && (context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager)))
                {
                    textBox.IsReadOnly = true;
                }
                else
                {
                    textBox.IsReadOnly = false;
                }

                var dropDownButton = (Button)datePicker.Template.FindName("PART_Button", datePicker);
                if (dropDownButton != null && (context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager)))
                {
                    dropDownButton.IsEnabled = false; // Hides the calendar drop-down button
                    dropDownButton.Visibility = Visibility.Collapsed; // Optional: Hide the button completely
                }

            }
        }
        private void EndDatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is DatePicker datePicker)
            {
                datePicker.IsEnabled = true;
                var textBox = (TextBox)datePicker.Template.FindName("PART_TextBox", datePicker);
                if (textBox != null && (context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager)))
                {
                    textBox.IsReadOnly = true;
                }
                else
                {
                    textBox.IsReadOnly = false;
                }

                var dropDownButton = (Button)datePicker.Template.FindName("PART_Button", datePicker);
                if (dropDownButton != null && (context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager)))
                {
                    dropDownButton.IsEnabled = false; // Hides the calendar drop-down button
                    dropDownButton.Visibility = Visibility.Collapsed; // Optional: Hide the button completely
                }
            }
        }
        private void LoadLeaveRequest()
        {


            if ((context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager)))
            {
                CommentTextBox.IsReadOnly = true;
                myButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                CommentTextBox.IsReadOnly = false;
            }

            if ((context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault() != LeaveRequestStatus.New || (context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.HRManager || context.Employes.Where(e => e.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager)))
            {
                comboBox.IsEnabled = true;


                comboBox.IsHitTestVisible = false;


                comboBox.IsEditable = false;
            }

            comboBox2.IsEnabled = true;


            comboBox2.IsHitTestVisible = false;


            comboBox2.IsEditable = false;

            var products = context.Employes.Where(e => e.Username == user).Select(x => x.FullName).ToList();
            IdTextBox.Text = id.ToString();
            EmployeeTextBox.Text = context.Employes.Where(e => e.Username == user).Select(x => x.FullName).FirstOrDefault();
            AbsenceReason k = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.AbsenceReason).FirstOrDefault();
            switch (k)
            {
                case AbsenceReason.A:
                    comboBox.SelectedIndex = 0;
                    break;
                case AbsenceReason.B:
                    comboBox.SelectedIndex = 1;
                    break;
                case AbsenceReason.C:
                    comboBox.SelectedIndex = 2;
                    break;
                case AbsenceReason.D:
                    comboBox.SelectedIndex = 3;
                    break;
            }

            StartDate.SelectedDate = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.StartDate).FirstOrDefault().ToDateTime(TimeOnly.Parse("10:00 PM"));
            EndDate.SelectedDate = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.EndDate).FirstOrDefault().ToDateTime(TimeOnly.Parse("10:00 PM"));
            CommentTextBox.Text = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Comment).FirstOrDefault();
            LeaveRequestStatus a = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault();
            switch (a)
            {
                case LeaveRequestStatus.New:
                    comboBox2.SelectedIndex = 0;
                    break;
                case LeaveRequestStatus.Approved:
                    comboBox2.SelectedIndex = 1;
                    break;
                case LeaveRequestStatus.Rejected:
                    comboBox2.SelectedIndex = 2;
                    break;
                case LeaveRequestStatus.Canceled:
                    comboBox2.SelectedIndex = 3;
                    break;
            }












        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
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
            MessageBox.Show("Leave request canceled");
            this.Close();



        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }
            if (comboBox.Text==""|| !StartDate.SelectedDate.HasValue||!EndDate.SelectedDate.HasValue|| comboBox2.Text == "")
            {
                MessageBox.Show("Fill in all required fields");
                
                return;


            }
            var leaveRequests = context.LeaveRequests.Where(x => x.Employee == context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefault()).Where(x => (x.Status == LeaveRequestStatus.New || x.Status == LeaveRequestStatus.Approved)).OrderBy(x => x.StartDate).ToList();
            leaveRequests=leaveRequests.Where(x=>x.Id!=id).ToList();
            bool isCandidateOverlaping = false;

            foreach (var leaveRequest in leaveRequests)
            {

                if (DateOnly.FromDateTime(StartDate.SelectedDate.Value) > leaveRequest.EndDate || DateOnly.FromDateTime(EndDate.SelectedDate.Value) < leaveRequest.StartDate)
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
                if (leaveRequests[0].StartDate > DateOnly.FromDateTime(EndDate.SelectedDate.Value) || leaveRequests[leaveRequests.Count - 1].EndDate < DateOnly.FromDateTime(StartDate.SelectedDate.Value))
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
                if (leaveRequests[i].EndDate < DateOnly.FromDateTime(StartDate.SelectedDate.Value) && leaveRequests[i + 1].StartDate > DateOnly.FromDateTime(EndDate.SelectedDate.Value))
                {
                    isSpace = true;
                }


            }
            if (!isSpace || isCandidateOverlaping)
            {
                MessageBox.Show("A specific date range covers a different New or Approved Leave Request.");
                return;


            }


            if (StartDate.SelectedDate.HasValue && EndDate.SelectedDate.HasValue)
            {
                if (StartDate.SelectedDate.Value > EndDate.SelectedDate.Value)
                {

                    MessageBox.Show("Start date must be earlier than end date.");
                    return;
                }
            }

            var obj = context.LeaveRequests.Find(id);
            switch (comboBox.Text)
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

            obj.StartDate = DateOnly.FromDateTime(StartDate.SelectedDate.Value);
            obj.EndDate = DateOnly.FromDateTime(EndDate.SelectedDate.Value);
            obj.Comment = CommentTextBox.Text;
            switch (comboBox2.Text)
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
            MessageBox.Show("Leave request updated");
            this.Close();



        }
        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        private void comboBox2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
