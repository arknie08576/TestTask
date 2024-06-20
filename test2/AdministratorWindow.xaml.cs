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

namespace test2
{
    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        private readonly IWindowService windowService;
        public AdministratorWindow(IWindowService _windowService)
        {
            InitializeComponent();
            windowService = _windowService;
        }
        private void Projects_Click(object sender, RoutedEventArgs e)
        {

            windowService.ShowWindow<ProjectsWindow>();
        }

        private void LeaveRequests_Click(object sender, RoutedEventArgs e)
        {
            windowService.ShowWindow<LeaveRequestsWindow>();

        }
        private void Employes_Click(object sender, RoutedEventArgs e)
        {
            windowService.ShowWindow<EmployesWindow>();

        }

        private void ApprovalRequests_Click(object sender, RoutedEventArgs e)
        {
            windowService.ShowWindow<ApprovalRequestsWindow>();

        }
    }
}
