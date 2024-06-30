using Microsoft.EntityFrameworkCore;
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
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Commands;
using test2.Data;
using test2.Enums;

namespace test2.ViewModels
{
    public class EditProjectViewModel : ViewModelBase, IParameterReceiver
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public string user;
        int id;
        public ICommand UpdateCommand { get; }
        public EditProjectViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            user = AuthenticationHelper.loggedUser;
            _windowService = windowService;
            Items = new ObservableCollection<string> { "A", "B", "C", "D" };
            Items3 = new ObservableCollection<string> { "Inactive", "Active" };
            var pms=context.Employes.Where(x=>x.Position==Position.ProjectManager).Select(x=>x.FullName).ToList();
            Items2 = new ObservableCollection<string>(pms);
            UpdateCommand = new RelayCommand<object>(OnUpdate);
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
        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        private ObservableCollection<string> _items;
        public ObservableCollection<string> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        private ObservableCollection<string> _items2;
        private string _selectedItem2;
        public string SelectedItem2
        {
            get => _selectedItem2;
            set => SetProperty(ref _selectedItem2, value);
        }

        public ObservableCollection<string> Items2
        {
            get => _items2;
            set => SetProperty(ref _items2, value);
        }


        private string _selectedItem3;
        public string SelectedItem3
        {
            get => _selectedItem3;
            set => SetProperty(ref _selectedItem3, value);
        }
        private ObservableCollection<string> _items3;
        public ObservableCollection<string> Items3
        {
            get => _items3;
            set => SetProperty(ref _items3, value);
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
        private void LoadEditProject()
        {
            var project = context.Projects.Where(e => e.Id == id).FirstOrDefault();
            Id = id.ToString();



            ProjectType k = context.Projects.Where(e => e.Id == id).Select(x => x.ProjectType).FirstOrDefault();
            switch (k)
            {
                case ProjectType.A:
                    SelectedItem = Items[0];
                    break;
                case ProjectType.B:
                    SelectedItem = Items[1];
                    break;
                case ProjectType.C:
                    SelectedItem = Items[2];
                    break;
                case ProjectType.D:
                    SelectedItem = Items[3];
                    break;
            }



            StartDate = project.StartDate.ToDateTime(TimeOnly.Parse("10:00 PM"));

            if (project.EndDate.HasValue)
            {
                EndDate = project.EndDate.Value.ToDateTime(TimeOnly.Parse("10:00 PM"));

            }
            var products = context.Employes.Where(e => e.Position == Position.ProjectManager).Select(x => x.FullName).ToList();
            
            var pm = context.Employes.Where(e => e.Id == project.ProjectManager).Select(x => x.FullName).FirstOrDefault();
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i] == pm)
                {

                    SelectedItem2 = Items2[i];

                }



            }




            Comment = project.Comment;

            ProjectStatus c = context.Projects.Where(e => e.Id == id).Select(x => x.ProjectStatus).FirstOrDefault();
            switch (c)
            {
                case ProjectStatus.Inactive:
                    SelectedItem3 = Items3[0];
                    break;
                case ProjectStatus.Active:
                    SelectedItem3 = Items3[1];
                    break;

            }


        }
        private void OnUpdate(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {
                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<EditProjectViewModel>();
                
                return;
            }
  
            var obj = context.Projects.Where(x => x.Id == id).FirstOrDefault();
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

            obj.StartDate = DateOnly.FromDateTime(StartDate.Value);
            if (EndDate.HasValue)
            {
                obj.EndDate = DateOnly.FromDateTime(EndDate.Value);
            }
            if (!string.IsNullOrEmpty(SelectedItem2))
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
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
          
            _dialogService.ShowMessage("Project updated..", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _windowService.CloseWindow<EditProjectViewModel>();
        }
        public async Task ReceiveParameterAsync(object parameter)
        {
            if (parameter is int data)
            {
                id = data;
                LoadEditProject();
            }
        }
    }
}
