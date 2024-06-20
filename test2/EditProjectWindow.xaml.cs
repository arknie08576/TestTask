using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for EditProjectWindow.xaml
    /// </summary>
    public partial class EditProjectWindow : Window
    {
        private readonly OfficeContex context;
        int id { get; set; }
        public EditProjectWindow(int Id, OfficeContex officeContex)
        {
            InitializeComponent();
            context = officeContex;
            this.id = Id;
            LoadEditProject();
        }
        private void LoadEditProject()
        {
            var project = context.Projects.Where(e => e.Id == id).FirstOrDefault();
            IdTextBox.Text = id.ToString();


           
            ProjectType k = context.Projects.Where(e => e.Id == id).Select(x => x.ProjectType).FirstOrDefault();
            switch (k)
            {
                case ProjectType.A:
                    comboBox.SelectedIndex = 0;
                    break;
                case ProjectType.B:
                    comboBox.SelectedIndex = 1;
                    break;
                case ProjectType.C:
                    comboBox.SelectedIndex = 2;
                    break;
                case ProjectType.D:
                    comboBox.SelectedIndex = 3;
                    break;
            }


          
                StartDatePicker.SelectedDate = project.StartDate.ToDateTime(TimeOnly.Parse("10:00 PM"));
            
            if (project.EndDate.HasValue)
            {
                EndDatePicker.SelectedDate = project.EndDate.Value.ToDateTime(TimeOnly.Parse("10:00 PM"));

            }
            var products = context.Employes.Where(e => e.Position == Position.ProjectManager).Select(x => x.FullName).ToList();
            comboBox2.ItemsSource = products;
            var pm = context.Employes.Where(e => e.Id == project.ProjectManager).Select(x => x.FullName).FirstOrDefault();
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i] == pm)
                {

                    comboBox2.SelectedIndex = i;

                }



            }




            CommentTextBox.Text = project.Comment;

            ProjectStatus c = context.Projects.Where(e => e.Id == id).Select(x => x.ProjectStatus).FirstOrDefault();
            switch (c)
            {
                case ProjectStatus.Inactive:
                    comboBox3.SelectedIndex = 0;
                    break;
                case ProjectStatus.Active:
                    comboBox3.SelectedIndex = 1;
                    break;

            }
















        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var obj = context.Projects.Where(x => x.Id == id).FirstOrDefault();
            switch (comboBox.Text)
            {
                case "A":
                    obj.ProjectType = ProjectType.A;
                    break;
                case "B":
                    obj.ProjectType = ProjectType.B;
                    break;
                case "C":
                    obj.ProjectType = ProjectType.C;
                    break;
                case "D":
                    obj.ProjectType = ProjectType.D;
                    break;

            }

            obj.StartDate = DateOnly.FromDateTime(StartDatePicker.SelectedDate.Value);
            obj.EndDate = DateOnly.FromDateTime(EndDatePicker.SelectedDate.Value);
            obj.ProjectManager = context.Employes.Where(x => x.FullName == comboBox2.Text).Select(x => x.Id).FirstOrDefault();
            obj.Comment = CommentTextBox.Text;
            switch (comboBox3.Text)
            {
                case "Inactive":
                    obj.ProjectStatus = ProjectStatus.Inactive;
                    break;
                case "Active":
                    obj.ProjectStatus = ProjectStatus.Active;
                    break;



            }
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            MessageBox.Show("Project updated.");
            this.Close();




        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ComboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
