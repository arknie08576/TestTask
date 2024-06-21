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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using test2.Models;

namespace test2
{
  
    public partial class NewLeaveRequestWindow : Window
    {
        string user;
        private readonly OfficeContex context;
        public NewLeaveRequestWindow(OfficeContex officeContex)
        {
            InitializeComponent();

            context = officeContex;
            user = AuthenticationHelper.loggedUser;
            LoadNewLeaveRequest();
        }
        private void LoadNewLeaveRequest()
        {

                EmployeeTextBox.Text = context.Employes.Where(e => e.Username == user).Select(x => x.FullName).ToList()[0];
            

        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

              
                var obj = new LeaveRequest();
                obj.Employee= context.Employes.Where(e => e.Username == user).Select(x => x.Id).FirstOrDefault();
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

                context.LeaveRequests.Add(obj);
            context.SaveChanges();
            var ar = new ApprovalRequest();
            ar.LeaveRequest = obj.Id;
            ar.Status=ApprovalRequestStatus.New;
            context.ApprovalRequests.Add(ar);
                context.SaveChanges();

            MessageBox.Show("Leave request added.");
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
