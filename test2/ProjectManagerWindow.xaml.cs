﻿using Microsoft.VisualBasic.ApplicationServices;
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
    /// Interaction logic for ProjectManagerWindow.xaml
    /// </summary>
    public partial class ProjectManagerWindow : Window
    {
        string user;
        private readonly IWindowService windowService;
        public ProjectManagerWindow(IWindowService _windowService)
        {
            InitializeComponent();
            windowService = _windowService;
            user = AuthenticationHelper.loggedUser;
        }
        private void Projects_Click(object sender, RoutedEventArgs e)
        {
            //ProjectsWindow projectsAppWindow = new ProjectsWindow();
          //  projectsAppWindow.Show();
            windowService.ShowWindow<ProjectsWindow>();

        }

        private void LeaveRequests_Click(object sender, RoutedEventArgs e)
        {
            //LeaveRequestsWindow projectsAppWindow = new LeaveRequestsWindow(user);
            //projectsAppWindow.Show();
            windowService.ShowWindow<LeaveRequestsWindow>();

        }
        private void Employes_Click(object sender, RoutedEventArgs e)
        {
           // EmployesWindow eWindow = new EmployesWindow(user);
            //eWindow.Show();
            windowService.ShowWindow<EmployesWindow>();

        }

        private void ApprovalRequests_Click(object sender, RoutedEventArgs e)
        {
            // ApprovalRequestsWindow eWindow = new ApprovalRequestsWindow(user);
            //eWindow.Show();
            windowService.ShowWindow<ApprovalRequestsWindow>();

        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // MainWindow mainAppWindow = new MainWindow();
            // mainAppWindow.Show();
            windowService.ShowWindow<MainWindow>();
            this.Close();

        }
    }
}
