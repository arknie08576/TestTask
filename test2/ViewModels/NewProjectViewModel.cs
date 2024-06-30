using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using test2.Models;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;
using Microsoft.EntityFrameworkCore;

namespace test2.ViewModels
{
    public class NewProjectViewModel : ViewModelBase
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public string user;
        public NewProjectViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;

            _windowService = windowService;
            Items = new ObservableCollection<string> { "A", "B", "C", "D" };
            Items3 = new ObservableCollection<string> { "Inactive", "Active" };
            
            AddProjectCommand = new AsyncRelayCommand<object>(OnAddProjectAsync);
            Task.Run(LoadItems2Async);
        }
        private async Task LoadItems2Async()
        {
            var pms = await context.Employes.Where(x => x.Position == Position.ProjectManager).Select(x => x.FullName).ToListAsync();
            Items2 = new ObservableCollection<string>(pms);

        }
        public ICommand AddProjectCommand { get; }
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
        private ObservableCollection<string> _items;
        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        public string SelectedItem2
        {
            get => _selectedItem2;
            set => SetProperty(ref _selectedItem2, value);
        }
        public string SelectedItem3
        {
            get => _selectedItem3;
            set => SetProperty(ref _selectedItem3, value);
        }
        public ObservableCollection<string> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        private ObservableCollection<string> _items2;
        private string _selectedItem2;

        public ObservableCollection<string> Items2
        {
            get => _items2;
            set => SetProperty(ref _items2, value);
        }
        private ObservableCollection<string> _items3;
        private string _selectedItem3;

        public ObservableCollection<string> Items3
        {
            get => _items3;
            set => SetProperty(ref _items3, value);
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
        private async Task OnAddProjectAsync(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<NewProjectViewModel>();
                return;
            }

            if (string.IsNullOrEmpty(SelectedItem) || !StartDate.HasValue || string.IsNullOrEmpty(SelectedItem3))
            {

                
                _dialogService.ShowMessage("Fill in all required fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            var obj = new Project();
            switch (SelectedItem)
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
            if (StartDate.HasValue)
            {
                obj.StartDate = DateOnly.FromDateTime(StartDate.Value);
            }
            if (EndDate.HasValue)
            {
                obj.EndDate = DateOnly.FromDateTime(EndDate.Value);
            }
            if (string.IsNullOrEmpty(SelectedItem2))
            {
                obj.ProjectManager = context.Employes.Where(x => x.FullName == SelectedItem2).Select(x => x.Id).FirstOrDefault();
            }
            obj.Comment = Comment;
            switch (SelectedItem3)
            {
                case "Inactive":
                    obj.ProjectStatus = ProjectStatus.Inactive;
                    break;
                case "Active":
                    obj.ProjectStatus = ProjectStatus.Active;
                    break;



            }
            await context.Projects.AddAsync(obj);
            
            await context.SaveChangesAsync();
            _dialogService.ShowMessage("Project added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<NewProjectViewModel>();
        }
    }
}
