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
using test2.TableModels;

namespace test2
{
    /// <summary>
    /// Interaction logic for LeaveRequestsWindow.xaml
    /// </summary>
    public partial class LeaveRequestsWindow : Window
    {
      //  string user;
      //  private readonly OfficeContex context;
       // private readonly IWindowService windowService;
        public LeaveRequestsWindow(OfficeContex officeContex, IWindowService _windowService)
        {
            InitializeComponent();
           // context= officeContex;
           // user = AuthenticationHelper.loggedUser;
           // windowService = _windowService;
            //LoadLeaveRequests();

        }
        /*
        private void LoadLeaveRequests()
        {

                var leaveRequests = context.LeaveRequests.ToList();
            if (context.Employes.Where(x => x.Username == user).FirstOrDefault().Position == Position.Employee)
            {
                leaveRequests = leaveRequests.Where(x => x.Employee== context.Employes.Where(x => x.Username == user).Select(x=>x.Id).FirstOrDefault()).ToList();

            }
            var viewleaveRequests = new List<ViewLeaveRequest>();
                foreach (var leaveRequest in leaveRequests)
                {

                    ViewLeaveRequest p = new ViewLeaveRequest
                    {
                        Id = leaveRequest.Id,
                        Employee = context.Employes.Where(x => x.Id == leaveRequest.Employee).Select(x => x.FullName).FirstOrDefault(),
                        AbsenceReasonn = leaveRequest.AbsenceReason,
                        StartDate = leaveRequest.StartDate,
                        EndDate = leaveRequest.EndDate,
                        Comment = leaveRequest.Comment,
                        Status = leaveRequest.Status


                    };
                    viewleaveRequests.Add(p);


                }



                ProjectDataGrid.ItemsSource = viewleaveRequests;

            









        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }
            var leaveRequests = context.LeaveRequests.ToList();
            if (context.Employes.Where(x => x.Username == user).FirstOrDefault().Position == Position.Employee)
            {
                leaveRequests = leaveRequests.Where(x => x.Employee == context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefault()).ToList();

            }
            var viewleaveRequests = new List<ViewLeaveRequest>();
                foreach (var leaveRequest in leaveRequests)
                {

                    ViewLeaveRequest p = new ViewLeaveRequest
                    {
                        Id = leaveRequest.Id,
                        Employee = context.Employes.Where(x => x.Id == leaveRequest.Employee).Select(x => x.FullName).FirstOrDefault(),
                        AbsenceReasonn = leaveRequest.AbsenceReason,
                        StartDate = leaveRequest.StartDate,
                        EndDate = leaveRequest.EndDate,
                        Comment = leaveRequest.Comment,
                        Status = leaveRequest.Status


                    };
                    viewleaveRequests.Add(p);


                }
                if (IdTextBox.Text != "")
                {

                    viewleaveRequests = viewleaveRequests.Where(x => x.Id == Convert.ToInt32(IdTextBox.Text)).ToList();
                }

                if (EmployeeTextBox.Text != "")
                {

                    viewleaveRequests = viewleaveRequests.Where(x => x.Employee == EmployeeTextBox.Text).ToList();
                }
                AbsenceReason x = AbsenceReason.A;

                switch (AbsenceReasonTextBox.Text)
                {
                    case "A":
                        x = AbsenceReason.A;
                        break;
                    case "B":
                        x = AbsenceReason.B;
                        break;
                    case "C":
                        x = AbsenceReason.C;
                        break;
                    case "D":
                        x = AbsenceReason.D;
                        break;
                    case "":
                        break;
                    default:
                        viewleaveRequests = new List<ViewLeaveRequest> { };
                        break;



                }

                if (AbsenceReasonTextBox.Text != "")
                {

                    viewleaveRequests = viewleaveRequests.Where(y => y.AbsenceReasonn == x).ToList();
                }
                if (StartDateTextBox.Text != "")
                {
                    string dateString = StartDateTextBox.Text;
                    string format = "dd/MM/yyyy";
                    if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                    {
                        viewleaveRequests = viewleaveRequests.Where(y => y.StartDate == date).ToList();
                    }

                }
                if (EndDateTextBox.Text != "")
                {
                    string dateString = EndDateTextBox.Text;
                    string format = "dd/MM/yyyy";
                    if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                    {
                        viewleaveRequests = viewleaveRequests.Where(y => y.EndDate == date).ToList();
                    }

                }
                if (CommentTextBox.Text != "")
                {

                    viewleaveRequests = viewleaveRequests.Where(x => x.Comment == CommentTextBox.Text).ToList();
                }
                LeaveRequestStatus f = LeaveRequestStatus.New;
            switch (StatusTextBox.Text)
                {
                    case "New":
                        f = LeaveRequestStatus.New;
                    break;
                    case "Approved":
                        f = LeaveRequestStatus.Approved;
                        break;
                    case "Rejected":
                        f = LeaveRequestStatus.Rejected;
                        break;
                    case "Canceled":
                        f = LeaveRequestStatus.Canceled;
                        break;
                    case "":
                        break;
                    default:
                        viewleaveRequests = new List<ViewLeaveRequest> { };
                        break;



                }
                if (StatusTextBox.Text != "")
                {

                    viewleaveRequests = viewleaveRequests.Where(y => y.Status == f).ToList();
                }

                ProjectDataGrid.ItemsSource = viewleaveRequests;

            





        }
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }
            if (ProjectDataGrid.SelectedItem is ViewLeaveRequest selectedData)
            {

                MessageBox.Show($"You double-clicked on: {selectedData.Id}");

                
               // windowService.ShowWindow<EditLeaveRequestWindow>(selectedData.Id);

            }
        }
        private void NewLeaveRequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }
            windowService.ShowWindow<NewLeaveRequestWindow>();


        }*/
    }
}
