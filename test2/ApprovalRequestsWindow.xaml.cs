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
using test2.TableModels;

namespace test2
{
    /// <summary>
    /// Interaction logic for ApprovalRequestsWindow.xaml
    /// </summary>
    public partial class ApprovalRequestsWindow : Window
    {
        string user;
        private readonly OfficeContex context;
        private readonly IWindowService windowService;
        public ApprovalRequestsWindow(OfficeContex officeContex, IWindowService _windowService)
        {
            this.user = AuthenticationHelper.loggedUser;
            this.context = officeContex;
            windowService = _windowService;
            InitializeComponent();
            this.user = user;
           // LoadApprovalRequests();
        }
        /*
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
                    LeaveRequestt = approvalRequest.LeaveRequest,
                    Status = approvalRequest.Status,
                    Comment = approvalRequest.Comment




                };
                viewapprovalRequests.Add(p);


            }

            ProjectDataGrid.ItemsSource = viewapprovalRequests;











        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }

            var approvalRequests = context.ApprovalRequests.ToList();
            var viewapprovalRequests = new List<ViewApprovalRequest>();
            foreach (var approvalRequest in approvalRequests)
            {

                ViewApprovalRequest p = new ViewApprovalRequest
                {
                    Id = approvalRequest.Id,
                    Approver = context.Employes.Where(x => x.Id == approvalRequest.Approver).Select(x => x.FullName).FirstOrDefault(),
                    LeaveRequestt = approvalRequest.LeaveRequest,
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

                viewapprovalRequests = viewapprovalRequests.Where(x => x.LeaveRequestt.ToString() == LeaveRequestTextBox.Text).ToList();
            }





            ApprovalRequestStatus x = ApprovalRequestStatus.New;

            switch (StatusTextBox.Text)
            {
                case "New":
                    x = ApprovalRequestStatus.New;
                    break;
                case "Approved":
                    x = ApprovalRequestStatus.Approved;
                    break;
                case "Rejected":
                    x = ApprovalRequestStatus.Rejected;
                    break;
                case "Canceled":
                    x = ApprovalRequestStatus.Canceled;
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
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }
            if (ProjectDataGrid.SelectedItem is ViewApprovalRequest selectedData)
            {

                MessageBox.Show($"You double-clicked on: {selectedData.Id}");


              //  windowService.ShowWindow<OpenApprovalRequestWindow>(selectedData.Id);
            }
        }
        private void NewLeaveRequestButton_Click(object sender, RoutedEventArgs e)
        {

            windowService.ShowWindow<NewLeaveRequestWindow>();



        }*/
    }
}

