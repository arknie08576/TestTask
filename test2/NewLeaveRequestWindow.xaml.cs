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
       // string user;
       // private readonly OfficeContex context;
        public NewLeaveRequestWindow(OfficeContex officeContex)
        {
            InitializeComponent();

            // context = officeContex;
            // user = AuthenticationHelper.loggedUser;
            //LoadNewLeaveRequest();
        }/*
        private void LoadNewLeaveRequest()
        {

            EmployeeTextBox.Text = context.Employes.Where(e => e.Username == user).Select(x => x.FullName).ToList()[0];
            comboBox2.IsEnabled = false;
            comboBox2.SelectedIndex = 0;

        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }
            if (!StartDate.SelectedDate.HasValue || !EndDate.SelectedDate.HasValue || comboBox.Text == "")
            {
                MessageBox.Show("Fill in all required fields.");
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

            var leaveRequests = context.LeaveRequests.Where(x => x.Employee == context.Employes.Where(x => x.Username == user).Select(x => x.Id).FirstOrDefault()).Where(x => (x.Status == LeaveRequestStatus.New || x.Status == LeaveRequestStatus.Approved)).OrderBy(x => x.StartDate).ToList();
            
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
            for(int i = 0; i < leaveRequests.Count - 2; i++)
            {
                if (leaveRequests[i].EndDate< DateOnly.FromDateTime(StartDate.SelectedDate.Value)&& leaveRequests[i+1].StartDate > DateOnly.FromDateTime(EndDate.SelectedDate.Value))
                {
                    isSpace = true;
                }


            }
            if (!isSpace|| isCandidateOverlaping)
            {
                MessageBox.Show("A specific date range covers a different New or Approved Leave Request.");
                return;


            }



                var obj = new LeaveRequest();
                obj.Employee = context.Employes.Where(e => e.Username == user).Select(x => x.Id).FirstOrDefault();
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
                obj.Status = LeaveRequestStatus.New;


                context.LeaveRequests.Add(obj);
                context.SaveChanges();
                var ar = new ApprovalRequest();
                ar.LeaveRequest = obj.Id;
                ar.Status = ApprovalRequestStatus.New;
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

            }*/
    }
    }
