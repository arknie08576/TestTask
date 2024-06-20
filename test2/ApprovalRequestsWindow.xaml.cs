using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
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
    /// Interaction logic for ApprovalRequestsWindow.xaml
    /// </summary>
    public partial class ApprovalRequestsWindow : Window
    {
        string user;
        private readonly OfficeContex context;
        public ApprovalRequestsWindow(OfficeContex officeContex)
        {
            this.user = AuthenticationHelper.loggedUser;
            this.context = officeContex;
            InitializeComponent();
            this.user = user;
            LoadApprovalRequests();
        }
        private void LoadApprovalRequests()
        {
            
                var approvalRequests = context.ApprovalRequests.ToList();
                var viewapprovalRequests = new List<ViewApprovalRequest>();
                foreach (var approvalRequest in approvalRequests)
                {

                    ViewApprovalRequest p = new ViewApprovalRequest
                    {
                        Id = approvalRequest.Id,
                        Approver = context.Employes.Where(x => x.Id == approvalRequest.Approver).Select(x => x.FullName).FirstOrDefault(),
                        LeaveRequest = approvalRequest.LeaveRequest,
                        Status = approvalRequest.Status,
                        Comment = approvalRequest.Comment




                    };
                    viewapprovalRequests.Add(p);


                }

                ProjectDataGrid.ItemsSource = viewapprovalRequests;

            









        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            
                var approvalRequests = context.ApprovalRequests.ToList();
                var viewapprovalRequests = new List<ViewApprovalRequest>();
                foreach (var approvalRequest in approvalRequests)
                {

                    ViewApprovalRequest p = new ViewApprovalRequest
                    {
                        Id = approvalRequest.Id,
                        Approver = context.Employes.Where(x => x.Id == approvalRequest.Approver).Select(x => x.FullName).FirstOrDefault(),
                        LeaveRequest = approvalRequest.LeaveRequest,
                        Status = approvalRequest.Status,
                        Comment = approvalRequest.Comment




                    };
                    viewapprovalRequests.Add(p);


                }



                if (IdTextBox.Text != "")
                {

                    viewapprovalRequests = viewapprovalRequests.Where(x => x.Id == Convert.ToInt32(IdTextBox.Text)).ToList();
                }

                if (ApproverTextBox.Text != "")
                {

                    viewapprovalRequests = viewapprovalRequests.Where(x => x.Approver == ApproverTextBox.Text).ToList();
                }
                if (LeaveRequestTextBox.Text != "")
                {

                    viewapprovalRequests = viewapprovalRequests.Where(x => x.LeaveRequest.ToString() == LeaveRequestTextBox.Text).ToList();
                }





                ApprovalRequestStatus x = ApprovalRequestStatus.Active;

                switch (StatusTextBox.Text)
                {
                    case "Inactive":
                        x = ApprovalRequestStatus.Inactive;
                        break;
                    case "Active":
                        x = ApprovalRequestStatus.Active;
                        break;
                    case "":
                        break;
                    default:
                        viewapprovalRequests = new List<ViewApprovalRequest> { };
                        break;



                }

                if (StatusTextBox.Text != "")
                {

                    viewapprovalRequests = viewapprovalRequests.Where(y => y.Status == x).ToList();
                }

                if (CommentTextBox.Text != "")
                {

                    viewapprovalRequests = viewapprovalRequests.Where(x => x.Comment == CommentTextBox.Text).ToList();
                }

                ProjectDataGrid.ItemsSource = viewapprovalRequests;

            

        }

        private void NewApprovalRequestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProjectDataGrid.SelectedItem is ViewApprovalRequest selectedData)
            {

                MessageBox.Show($"You double-clicked on: {selectedData.Id}");

               // OpenApprovalRequestWindow oLRWindow = new OpenApprovalRequestWindow(user, selectedData.Id);
               // oLRWindow.Show();

            }
        }
        private void NewLeaveRequestButton_Click(object sender, RoutedEventArgs e)
        {
            //NewLeaveRequestWindow nLRWindow = new NewLeaveRequestWindow(user);
            //nLRWindow.Show();



        }
    }
}

