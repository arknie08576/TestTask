﻿using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow : Window
    {
        private readonly OfficeContex context;
        public NewProjectWindow(OfficeContex officeContex)
        {
            InitializeComponent();
            context = officeContex;
            Load();
        }


        private void Load()
        {

                var products = context.Employes.Where(e => e.Position == Position.ProjectManager).Select(x => x.FullName).ToList();
                comboBox2.ItemsSource = products;
            


        }
        private void AddProjectButton_Click(object sender, RoutedEventArgs e)
        {

                var obj = new Project();
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
                context.Projects.Add(obj);
                context.SaveChanges();
                MessageBox.Show("Project added.");
                this.Close();
            
        }
    }
}
