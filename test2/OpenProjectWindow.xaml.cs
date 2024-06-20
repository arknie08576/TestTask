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
    /// Interaction logic for OpenProjectWindow.xaml
    /// </summary>
    /// 

    public partial class OpenProjectWindow : Window
    {
        int id;
        private readonly OfficeContex context;
        public OpenProjectWindow(int Id, OfficeContex officeContex )
        {
            InitializeComponent();
            this.id = Id;
            
            context = officeContex;
            LoadOpenProject();
        }
        private void LoadOpenProject()
        {

                var project = context.Projects.Where(e => e.Id == id).ToList()[0];
                IdTextBox.Text = id.ToString();
                ProjectTypeTextBox.Text=project.ProjectType.ToString();
                StartDatePicker.SelectedDate=project.StartDate.ToDateTime(TimeOnly.Parse("10:00 PM"));
                
                if (project.EndDate.HasValue)
                {
                    EndDatePicker.SelectedDate = project.EndDate.Value.ToDateTime(TimeOnly.Parse("10:00 PM"));

                }

                ProjectManagerTextBox.Text = context.Employes.Where(e => e.Id == project.ProjectManager).Select(x => x.FullName).FirstOrDefault();
                CommentTextBox.Text = project.Comment;
                ProjectStatusTextBox.Text=project.ProjectStatus.ToString();





            




        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
