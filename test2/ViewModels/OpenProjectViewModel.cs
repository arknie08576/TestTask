using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using test2.Models;
using test2.Views;
using test2.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using test2.Services;
using test2.Interfaces;
using test2.Data;

namespace test2.ViewModels
{
    public class OpenProjectViewModel : INotifyPropertyChanged, IParameterReceiver
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
        public string user;
        int id;

        public OpenProjectViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;

            _windowService = windowService;
           // Items = new ObservableCollection<string> { "A", "B", "C", "D" };
           // Items3 = new ObservableCollection<string> { "Inactive", "Active" };
           // var pms = context.Employes.Where(x => x.Position == Position.ProjectManager).Select(x => x.FullName).ToList();
           // Items2 = new ObservableCollection<string>(pms);
            //  SelectedItem2 = Items[0];
            // Employee = context.Employes.Where(e => e.Username == user).Select(x => x.FullName).FirstOrDefault();
            // Initialize commands
           // UpdateCommand = new RelayCommand<object>(OnUpdate);
            // StartDate=DateTime.Now;
            // EndDate=DateTime.Now;
            //CloseCommand = new RelayCommand<object>(Close);
        }
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _projectTypee;
        public string ProjectTypee
        {
            get => _projectTypee;
            set
            {
                _projectTypee = value;
                OnPropertyChanged(nameof(ProjectTypee));
            }
        }
        private string _projectManager;
        public string ProjectManager
        {
            get => _projectManager;
            set
            {
                _projectManager = value;
                OnPropertyChanged(nameof(ProjectManager));
            }
        }
        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        private string _projectStatuss;
        public string ProjectStatuss
        {
            get => _projectStatuss;
            set
            {
                _projectStatuss = value;
                OnPropertyChanged(nameof(ProjectStatuss));
            }
        }
        private DateTime? _startDate;

        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        private DateTime? _endDate;

        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }
        private void LoadOpenProject()
        {

            var project = context.Projects.Where(e => e.Id == id).ToList()[0];
            Id = id.ToString();
            ProjectTypee = project.ProjectType.ToString();
            StartDate = project.StartDate.ToDateTime(TimeOnly.Parse("10:00 PM"));

            if (project.EndDate.HasValue)
            {
                EndDate = project.EndDate.Value.ToDateTime(TimeOnly.Parse("10:00 PM"));

            }

            ProjectManager = context.Employes.Where(e => e.Id == project.ProjectManager).Select(x => x.FullName).FirstOrDefault();
            Comment = project.Comment;
            ProjectStatuss = project.ProjectStatus.ToString();










        }

        public async Task ReceiveParameterAsync(object parameter)
        {
            if (parameter is int data)
            {
                id = data;

                LoadOpenProject();
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
