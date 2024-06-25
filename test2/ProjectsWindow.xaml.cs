using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    /// Interaction logic for ProjectsWindow.xaml
    /// </summary>
    public partial class ProjectsWindow : Window
    {
        public string user;
        private readonly OfficeContex context;
        private readonly IWindowService windowService;
        public ProjectsWindow(OfficeContex officeContex, IWindowService _windowService)
        {
            user = AuthenticationHelper.loggedUser;
            context = officeContex;
            windowService = _windowService;
            InitializeComponent();
            LoadProjects();
        }
        private void LoadProjects()
        {

            var u = context.Employes.Where(x => x.Username == user).FirstOrDefault();

            if (u.Position != Position.ProjectManager&& u.Position != Position.Administrator)
            {
                myButton.Visibility = Visibility.Collapsed;

            }



            var projects = context.Projects.ToList();
            var viewprojects = new List<ViewProject>();
            foreach (var project in projects)
            {

                ViewProject p = new ViewProject
                {
                    Id = project.Id,
                    ProjectType = project.ProjectType,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    ProjectManager = context.Employes.Where(e => e.Position == Position.ProjectManager).Where(x => x.Id == project.ProjectManager).Select(x => x.FullName).FirstOrDefault(),
                    Comment = project.Comment,
                    ProjectStatus = project.ProjectStatus




                };
                viewprojects.Add(p);


            }

            ProjectDataGrid2.ItemsSource = viewprojects;


            
        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {


        }

        private void FilterTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {





        }
        private void NewProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }

            windowService.ShowWindow<NewProjectWindow>();
        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }
            var projects = context.Projects.ToList();
            var viewprojects = new List<ViewProject>();
            foreach (var project in projects)
            {

                ViewProject p = new ViewProject
                {
                    Id = project.Id,
                    ProjectType = project.ProjectType,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    ProjectManager = context.Employes.Where(e => e.Position == Position.ProjectManager).Where(x => x.Id == project.ProjectManager).Select(x => x.FullName).FirstOrDefault(),
                    Comment = project.Comment,
                    ProjectStatus = project.ProjectStatus




                };
                viewprojects.Add(p);


            }



            ProjectType x = ProjectType.A;

            switch (ProjectTypeTextBox.Text)
            {
                case "A":
                    x = ProjectType.A;
                    break;
                case "B":
                    x = ProjectType.B;
                    break;
                case "C":
                    x = ProjectType.C;
                    break;
                case "D":
                    x = ProjectType.D;
                    break;
                case "":
                    break;
                default:
                    viewprojects = new List<ViewProject> { };
                    break;



            }

            if (ProjectTypeTextBox.Text != "")
            {

                viewprojects = viewprojects.Where(y => y.ProjectType == x).ToList();
            }

            if (StartDateTextBox.Text != "")
            {
                string dateString = StartDateTextBox.Text;
                string format = "dd/MM/yyyy";
                if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                {
                    viewprojects = viewprojects.Where(y => y.StartDate == date).ToList();
                }

            }
            if (EndDateTextBox.Text != "")
            {
                string dateString = EndDateTextBox.Text;
                string format = "dd/MM/yyyy";
                if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                {
                    viewprojects = viewprojects.Where(y => y.EndDate == date).ToList();
                }

            }

            if (ProjectManagerTextBox.Text != "")
            {

                viewprojects = viewprojects.Where(x => x.ProjectManager == ProjectManagerTextBox.Text).ToList();
            }

            if (CommentTextBox.Text != "")
            {

                viewprojects = viewprojects.Where(x => x.Comment == CommentTextBox.Text).ToList();
            }

            ProjectStatus s = ProjectStatus.Active;
            switch (ProjectStatusTextBox.Text)
            {
                case "Active":
                    s = ProjectStatus.Active;
                    break;
                case "Inactive":
                    s = ProjectStatus.Inactive;
                    break;
                case "":
                    break;
                default:
                    viewprojects = new List<ViewProject> { };
                    break;



            }

            if (ProjectStatusTextBox.Text != "")
            {

                viewprojects = viewprojects.Where(y => y.ProjectStatus == s).ToList();
            }





            ProjectDataGrid2.ItemsSource = viewprojects;


        }
        private void DataGrid2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                MessageBox.Show("User logged out");
                this.Close();
                return;
            }

            if (ProjectDataGrid2.SelectedItem is ViewProject selectedData)
            {

                MessageBox.Show($"You double-clicked on: {selectedData.Id}");


                if (context.Employes.Where(x => x.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager|| context.Employes.Where(x => x.Username == user).Select(x => x.Position).FirstOrDefault() == Position.Administrator)
                {
                    
                    windowService.ShowWindow<EditProjectWindow>(selectedData.Id);

                }
                else
                {
                    
                    windowService.ShowWindow<OpenProjectWindow>(selectedData.Id);
                }





            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
