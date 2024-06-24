using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using test2.Models;

namespace test2
{

    public partial class OpenApprovalRequestWindow : Window
    {
        string user;
        int id;
        private readonly OfficeContex context;
        public OpenApprovalRequestWindow(int Id, OfficeContex officeContex)
        {
            InitializeComponent();
            user = AuthenticationHelper.loggedUser;
            context = officeContex;
            id = Id;
            LoadApprovalRequest();

        }

        private void LoadApprovalRequest()
        {



            IdTextBox.Text = id.ToString();
            var t = context.ApprovalRequests.Where(e => e.Id == id).Select(x => x.Approver).FirstOrDefault();
            ApproverTextBox.Text = context.Employes.Where(e => e.Id == t).Select(x => x.FullName).FirstOrDefault();
            LeaverequestTextBox.Text = context.ApprovalRequests.Where(e => e.Id == id).Select(x => x.LeaveRequest).FirstOrDefault().ToString();


            ApprovalRequestStatus k = context.ApprovalRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault();
            switch (k)
            {
                case ApprovalRequestStatus.New:
                    comboBox.SelectedIndex = 0;
                    break;
                case ApprovalRequestStatus.Approved:
                    comboBox.SelectedIndex = 1;
                    ApproveButton.Visibility = Visibility.Collapsed;
                    RejectButton.Visibility = Visibility.Collapsed;
                    break;
                case ApprovalRequestStatus.Rejected:
                    comboBox.SelectedIndex = 2;
                    ApproveButton.Visibility = Visibility.Collapsed;
                    RejectButton.Visibility = Visibility.Collapsed;
                    break;
                case ApprovalRequestStatus.Canceled:
                    comboBox.SelectedIndex = 3;
                    ApproveButton.Visibility = Visibility.Collapsed;
                    RejectButton.Visibility = Visibility.Collapsed;
                    break;
            }
            comboBox.IsEnabled = false;
            CommentTextBox.Text = context.ApprovalRequests.Where(e => e.Id == id).Select(x => x.Comment).FirstOrDefault();












        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }
            var obj = context.ApprovalRequests.Find(id);
            obj.Approver = context.Employes.Where(e => e.Username == user).Select(x => x.Id).FirstOrDefault();
            obj.Status = ApprovalRequestStatus.Approved;
            obj.Comment = CommentTextBox.Text;
            var lr = context.LeaveRequests.Where(e => e.Id == obj.LeaveRequest).FirstOrDefault();
            var emp = context.Employes.Where((e) => e.Id == lr.Employee).FirstOrDefault();
            lr.Status = LeaveRequestStatus.Approved;
            emp.Out_of_OfficeBalance -= (obj.leaveRequest.EndDate.ToDateTime(TimeOnly.MinValue) - obj.leaveRequest.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1;
            context.Entry(lr).State = EntityState.Modified;
            context.Entry(emp).State = EntityState.Modified;
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            MessageBox.Show("Approval request approved");
            this.Close();

        }
        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
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
            obj.Comment = CommentTextBox.Text;


            lr.Status = LeaveRequestStatus.Rejected;
            //obj.leaveRequest.Status = LeaveRequestStatus.Rejected;
            context.Entry(lr).State = EntityState.Modified;
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            MessageBox.Show("Approval request rejected");
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
