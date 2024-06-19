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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace test2
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        string user;
        public EmployeeWindow(string username)
        {
            user = username;
            InitializeComponent();
        }

        private void Projects_Click(object sender, RoutedEventArgs e)
        {
            ProjectsWindow projectsAppWindow = new ProjectsWindow(user);
            projectsAppWindow.Show();

        }

        private void LeaveRequests_Click(object sender, RoutedEventArgs e)
        {
            LeaveRequestsWindow projectsAppWindow = new LeaveRequestsWindow(user);
            projectsAppWindow.Show();

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            // Perform your action here
          //  MainWindow mainAppWindow = new MainWindow();
           //  mainAppWindow.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
          //  MainWindow mainAppWindow = new MainWindow();
           // mainAppWindow.Show();
            this.Close();
        }
    }
}
