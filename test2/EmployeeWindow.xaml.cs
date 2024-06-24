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
   
    public partial class EmployeeWindow : Window
    {
        string user;
        private readonly IWindowService windowService;
        public EmployeeWindow(IWindowService _windowService)
        {
            windowService = _windowService;
            user = AuthenticationHelper.loggedUser;
            InitializeComponent();
        }

        private void Projects_Click(object sender, RoutedEventArgs e)
        {

            
            windowService.ShowWindow<ProjectsWindow>();

        }

        private void LeaveRequests_Click(object sender, RoutedEventArgs e)
        {

            
            windowService.ShowWindow<LeaveRequestsWindow>();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
           
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationHelper.loggedUser = null;
            windowService.ShowWindow<MainWindow>();
            this.Close();
        }
    }
}
