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
                    break;
                case ApprovalRequestStatus.Rejected:
                    comboBox.SelectedIndex = 2;
                    break;
                case ApprovalRequestStatus.Canceled:
                    comboBox.SelectedIndex = 3;
                    break;
            }
            CommentTextBox.Text = context.ApprovalRequests.Where(e => e.Id == id).Select(x => x.Comment).FirstOrDefault();












        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {


            var obj = context.ApprovalRequests.Find(id);
            switch (comboBox.Text)
            {
                case "New":
                    obj.Status = ApprovalRequestStatus.New;
                    break;
                case "Approved":
                    obj.Status = ApprovalRequestStatus.Approved;
                    break;
                case "Rejected":
                    obj.Status = ApprovalRequestStatus.Rejected;
                    break;
                case "Canceled":
                    obj.Status = ApprovalRequestStatus.Canceled;
                    break;

            }


            obj.Comment = CommentTextBox.Text;
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            MessageBox.Show("Approval request updated");
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
