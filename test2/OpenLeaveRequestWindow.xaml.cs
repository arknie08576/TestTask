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
    /// <summary>
    /// Interaction logic for OpenLeaveRequestWindow.xaml
    /// </summary>
    public partial class OpenLeaveRequestWindow : Window
    {
        string user;   
        int id;
        private readonly OfficeContex context;
        public OpenLeaveRequestWindow(int Id, OfficeContex officeContex)
        {
            InitializeComponent();
            user = AuthenticationHelper.loggedUser;
            context = officeContex;
            id = Id;
            LoadLeaveRequest();
           
        }

        private void LoadLeaveRequest(){


                var products = context.Employes.Where(e => e.Username == user).Select(x => x.FullName).ToList();
                IdTextBox.Text = id.ToString();
                EmployeeTextBox.Text= context.Employes.Where(e => e.Username == user).Select(x => x.FullName).FirstOrDefault();
                AbsenceReason k= context.LeaveRequests.Where(e => e.Id == id).Select(x => x.AbsenceReason).FirstOrDefault();
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
                CommentTextBox.Text= context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Comment).FirstOrDefault();
                LeaveRequestStatus a = context.LeaveRequests.Where(e => e.Id == id).Select(x => x.Status).FirstOrDefault();
                switch (a)
                {
                    case LeaveRequestStatus.A:
                        comboBox2.SelectedIndex = 1;
                        break;
                    case LeaveRequestStatus.B:
                        comboBox2.SelectedIndex = 2;
                        break;
                    case LeaveRequestStatus.C:
                        comboBox2.SelectedIndex = 3;
                        break;
                    case LeaveRequestStatus.New:
                        comboBox2.SelectedIndex = 0;
                        break;
                }





            






            }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {


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
                obj.Comment= CommentTextBox.Text;
                switch (comboBox2.Text)
                {
                    case "A":
                        obj.Status = LeaveRequestStatus.A;
                        break;
                    case "B":
                        obj.Status = LeaveRequestStatus.B;
                        break;
                    case "C":
                        obj.Status = LeaveRequestStatus.C;
                        break;
                    case "New":
                        obj.Status = LeaveRequestStatus.New;
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
