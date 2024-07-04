using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using test2.TableModels;
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;
using Microsoft.EntityFrameworkCore;


namespace test2.ViewModels
{
    public class EmployesViewModel : ViewModelBase
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        string user;
        public ICommand FilterCommand { get; }
        public ICommand NewEmployeeCommand { get; }
        public ICommand RowDoubleClickCommand { get; }
        public EmployesViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;

            _windowService = windowService;

            // Initialize commands
            FilterCommand = new AsyncRelayCommand<object>(OnFilterAsync);
            NewEmployeeCommand = new RelayCommand<object>(OnNewEmployee);
            RowDoubleClickCommand = new RelayCommand<ViewEmployee>(OnRowDoubleClick);
            //CloseCommand = new RelayCommand<object>(Close);
            Task.Run(LoadEmployesAsync);
            
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
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        private string _subdivisionn;
        public string Subdivisionn
        {
            get => _subdivisionn;
            set
            {
                _subdivisionn = value;
                OnPropertyChanged(nameof(Subdivisionn));
            }
        }
        private string _positionn;
        public string Positionn
        {
            get => _positionn;
            set
            {
                _positionn = value;
                OnPropertyChanged(nameof(Positionn));
            }
        }
        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        private string _peoplePartner;
        public string PeoplePartner
        {
            get => _peoplePartner;
            set
            {
                _peoplePartner = value;
                OnPropertyChanged(nameof(PeoplePartner));
            }
        }
        private string _outOfOfficeBalance;
        public string OutOfOfficeBalance
        {
            get => _outOfOfficeBalance;
            set
            {
                _outOfOfficeBalance = value;
                OnPropertyChanged(nameof(OutOfOfficeBalance));
            }
        }
        private string _assignedProject;
        public string AssignedProject
        {
            get => _assignedProject;
            set
            {
                _assignedProject = value;
                OnPropertyChanged(nameof(AssignedProject));
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
                    OnPropertyChanged(nameof(IsButtonVisible));
                }
            }
        }
        private ObservableCollection<ViewEmployee> _filteredEmployes;
        public ObservableCollection<ViewEmployee> FilteredEmployes
        {
            get { return _filteredEmployes; }
            set
            {
                if (_filteredEmployes != value)
                {
                    _filteredEmployes = value;
                    OnPropertyChanged(nameof(FilteredEmployes));
                }
            }
        }
        private async Task LoadEmployesAsync()
        {

            var ob = await context.Employes.Where(x => x.Username == user).FirstOrDefaultAsync();
            if (ob.Position == Position.HRManager)
            {



            }
            if (ob.Position == Position.ProjectManager)
            {
                IsButtonVisible = false;


            }
            else
            {
                IsButtonVisible = true;
            }
            var employes = await context.Employes.ToListAsync();
            var viewemployes = new List<ViewEmployee>();
            foreach (var employe in employes)
            {

                var peoplePartner = await context.Employes.Where(x => x.Id == employe.PeoplePartner).Select(x => x.FullName).FirstOrDefaultAsync();
                ViewEmployee p = new ViewEmployee
                {
                    Id = employe.Id,
                    Username = employe.Username,
                    PasswordHash = employe.PasswordHash,
                    Salt = employe.Salt,
                    FullName = employe.FullName,
                    Subdivisionn = employe.Subdivision,
                    Positionn = employe.Position,
                    Status = employe.Status,
                    PeoplePartner = peoplePartner,
                    Out_of_OfficeBalance = employe.Out_of_OfficeBalance,
                    //Photo = employe.Photo,
                    AssignedProject = employe.AssignedProject

                };
                viewemployes.Add(p);


            }

            FilteredEmployes = new ObservableCollection<ViewEmployee>(viewemployes);



        }
        private async Task OnFilterAsync(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<EmployesViewModel>();
                return;
            }
            var employes = await context.Employes.ToListAsync();
            var viewemployes = new List<ViewEmployee>();
            foreach (var employe in employes)
            {
                var peoplePartner = await context.Employes.Where(x => x.Id == employe.PeoplePartner).Select(x => x.FullName).FirstOrDefaultAsync();
                ViewEmployee p = new ViewEmployee
                {
                    Id = employe.Id,
                    Username = employe.Username,
                    PasswordHash = employe.PasswordHash,
                    Salt = employe.Salt,
                    FullName = employe.FullName,
                    Subdivisionn = employe.Subdivision,
                    Positionn = employe.Position,
                    Status = employe.Status,
                    PeoplePartner = peoplePartner,
                    Out_of_OfficeBalance = employe.Out_of_OfficeBalance,
                    //  Photo = employe.Photo,
                    AssignedProject = employe.AssignedProject

                };
                viewemployes.Add(p);


            }

            if (!string.IsNullOrEmpty(Id))
            {

                viewemployes = viewemployes.Where(y => y.Id.ToString() == Id).ToList();
            }
            if (!string.IsNullOrEmpty(Username))
            {

                viewemployes = viewemployes.Where(y => y.Username == Username).ToList();
            }


            if (!string.IsNullOrEmpty(FullName))
            {

                viewemployes = viewemployes.Where(y => y.FullName == FullName).ToList();
            }






            Subdivision x = Subdivision.A;

            switch (Subdivisionn)
            {
                case "A":
                    x = Subdivision.A;
                    break;
                case "B":
                    x = Subdivision.B;
                    break;
                case "C":
                    x = Subdivision.C;
                    break;
                case "D":
                    x = Subdivision.D;
                    break;
                case "E":
                    x = Subdivision.E;
                    break;
                case "F":
                    x = Subdivision.F;
                    break;
                case "":
                case null:
                    break;
                default:
                    viewemployes = new List<ViewEmployee> { };
                    break;



            }

            if (!string.IsNullOrEmpty(Subdivisionn))
            {

                viewemployes = viewemployes.Where(y => y.Subdivisionn == x).ToList();
            }


            Position y = Position.Employee;

            switch (Positionn)
            {
                case "Employee":
                    y = Position.Employee;
                    break;
                case "HRManager":
                    y = Position.HRManager;
                    break;
                case "ProjectManager":
                    y = Position.ProjectManager;
                    break;
                case "Administrator":
                    y = Position.Administrator;
                    break;
                case "":
                case null:
                    break;
                default:
                    viewemployes = new List<ViewEmployee> { };
                    break;



            }

            if (!string.IsNullOrEmpty(Positionn))
            {

                viewemployes = viewemployes.Where(x => x.Positionn == y).ToList();
            }

            EmployeeStatus s = EmployeeStatus.Active;

            switch (Status)
            {
                case "Active":
                    s = EmployeeStatus.Active;
                    break;
                case "Inactive":
                    s = EmployeeStatus.Inactive;
                    break;
                case "":
                case null:
                    break;
                default:
                    viewemployes = new List<ViewEmployee> { };
                    break;



            }

            if (!string.IsNullOrEmpty(Status))
            {

                viewemployes = viewemployes.Where(x => x.Status == s).ToList();
            }

            if (!string.IsNullOrEmpty(PeoplePartner))
            {

                viewemployes = viewemployes.Where(y => y.PeoplePartner == PeoplePartner).ToList();
            }


            if (!string.IsNullOrEmpty(OutOfOfficeBalance))
            {

                viewemployes = viewemployes.Where(y => y.Out_of_OfficeBalance.ToString() == OutOfOfficeBalance).ToList();
            }


            if (!string.IsNullOrEmpty(AssignedProject))
            {

                viewemployes = viewemployes.Where(y => y.AssignedProject.ToString() == AssignedProject).ToList();
            }

            FilteredEmployes = new ObservableCollection<ViewEmployee>(viewemployes);
        }
        private void OnNewEmployee(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<EmployesViewModel>();
                return;
            }
            _windowService.ShowWindow<RegisterViewModel>();
        }
        private void OnRowDoubleClick(ViewEmployee item)
        {
            if (item != null)
            {
                if (AuthenticationHelper.loggedUser == null)
                {
                    _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _windowService.CloseWindow<EmployesViewModel>();
                    return;
                }
                
                _windowService.ShowWindow<OpenEmployeeViewModel>(item.Id);
            }
        }
    }
}
