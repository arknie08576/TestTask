using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using test2.Models;
using test2.TableModels;
using test2.Views;
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;

namespace test2.ViewModels
{
    public class ProjectsViewModel : INotifyPropertyChanged
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
        string user;
        public ICommand FilterCommand { get; }
        public ICommand NewProjectCommand { get; }
        public ICommand RowDoubleClickCommand { get; }
        //var _projects = new ObservableCollection<ViewProject>(viewprojects);
        private ObservableCollection<ViewProject> _projects;
        public ProjectsViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;

            _windowService = windowService;

            // Initialize commands
            FilterCommand = new RelayCommand<object>(OnFilter);
            NewProjectCommand = new RelayCommand<object>(OnNewProject);
            RowDoubleClickCommand = new RelayCommand<ViewProject>(OnRowDoubleClick);
            //CloseCommand = new RelayCommand<object>(Close);
            LoadProjects();
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
        private string _startDate;
        public string StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private string _endDate;
        public string EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
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
        private bool _isButtonVisible;

        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set
            {
                if (_isButtonVisible != value)
                {
                    _isButtonVisible = value;
                    //OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<ViewProject> _filteredProjects;
        public ObservableCollection<ViewProject> FilteredProjects
        {
            get { return _filteredProjects; }
            set
            {
                if (_filteredProjects != value)
                {
                    _filteredProjects = value;
                    OnPropertyChanged(nameof(FilteredProjects));
                }
            }
        }

        private void LoadProjects()
        {
            var u = context.Employes.Where(x => x.Username == user).FirstOrDefault();

            if (u.Position != Position.ProjectManager && u.Position != Position.Administrator)
            {
                IsButtonVisible = false;

            }
            else
            {
                IsButtonVisible = true;
            }



            var projects = context.Projects.ToList();
            var viewprojects = new List<ViewProject>();
            foreach (var project in projects)
            {

                ViewProject p = new ViewProject
                {
                    Id = project.Id,
                    ProjectTypee = project.ProjectType,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    ProjectManager = context.Employes.Where(e => e.Position == Position.ProjectManager).Where(x => x.Id == project.ProjectManager).Select(x => x.FullName).FirstOrDefault(),
                    Comment = project.Comment,
                    ProjectStatuss = project.ProjectStatus




                };
                viewprojects.Add(p);


            }
            _projects = new ObservableCollection<ViewProject>(viewprojects);
            FilteredProjects = _projects;

        }
        private void OnFilter(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowService.CloseWindow<ProjectsViewModel>();
                return;
            }
            var projects = context.Projects.ToList();
            var viewprojects = new List<ViewProject>();
            foreach (var project in projects)
            {

                ViewProject p = new ViewProject
                {
                    Id = project.Id,
                    ProjectTypee = project.ProjectType,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    ProjectManager = context.Employes.Where(e => e.Position == Position.ProjectManager).Where(x => x.Id == project.ProjectManager).Select(x => x.FullName).FirstOrDefault(),
                    Comment = project.Comment,
                    ProjectStatuss = project.ProjectStatus




                };
                viewprojects.Add(p);


            }

            if (!string.IsNullOrEmpty(Id))
            {

                viewprojects = viewprojects.Where(y => y.Id.ToString() == Id).ToList();
            }

            ProjectType x = ProjectType.A;

            switch (ProjectTypee)
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
                case null:
                    break;
                default:
                    viewprojects = new List<ViewProject> { };
                    break;



            }

            if (!string.IsNullOrEmpty(ProjectTypee))
            {

                viewprojects = viewprojects.Where(y => y.ProjectTypee == x).ToList();
            }

            if (!string.IsNullOrEmpty(StartDate))
            {
                string dateString = StartDate;
                string format = "dd/MM/yyyy";
                if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                {
                    viewprojects = viewprojects.Where(y => y.StartDate == date).ToList();
                }

            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                string dateString = EndDate;
                string format = "dd/MM/yyyy";
                if (DateOnly.TryParseExact(dateString, format, null, DateTimeStyles.None, out DateOnly date))
                {
                    viewprojects = viewprojects.Where(y => y.EndDate == date).ToList();
                }

            }

            if (!string.IsNullOrEmpty(ProjectManager))
            {

                viewprojects = viewprojects.Where(x => x.ProjectManager == ProjectManager).ToList();
            }

            if (!string.IsNullOrEmpty(Comment))
            {

                viewprojects = viewprojects.Where(x => x.Comment == Comment).ToList();
            }

            ProjectStatus s = ProjectStatus.Active;
            switch (ProjectStatuss)
            {
                case "Active":
                    s = ProjectStatus.Active;
                    break;
                case "Inactive":
                    s = ProjectStatus.Inactive;
                    break;
                case "":
                case null:
                    break;
                default:
                    viewprojects = new List<ViewProject> { };
                    break;



            }

            if (!string.IsNullOrEmpty(ProjectStatuss))
            {

                viewprojects = viewprojects.Where(y => y.ProjectStatuss == s).ToList();
            }





            _projects = new ObservableCollection<ViewProject>(viewprojects);
            
            FilteredProjects = _projects;




        }
        private void OnNewProject(object parameter)
        {

            _windowService.ShowWindow<NewProjectViewModel>();

        }
        private void OnRowDoubleClick(ViewProject item)
        {
            
                // Perform your action here
              //  MessageBox.Show($"Double-clicked on: {item.Id}");
              //  _windowService.ShowWindow<EditLeaveRequestViewModel>(item.Id);

                if (AuthenticationHelper.loggedUser == null)
                {
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<ProjectsViewModel>();
                return;
               
                }

            if (item != null)
            {

                _dialogService.ShowMessage($"Double-clicked on: {item.Id}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


                if (context.Employes.Where(x => x.Username == user).Select(x => x.Position).FirstOrDefault() == Position.ProjectManager || context.Employes.Where(x => x.Username == user).Select(x => x.Position).FirstOrDefault() == Position.Administrator)
                    {

                     _windowService.ShowWindow<EditProjectViewModel>(item.Id);

                }
                else
                    {

                    _windowService.ShowWindow<OpenProjectViewModel>(item.Id);
                }





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
