using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using test2.View;

namespace test2.ViewModels
{
    public class NewLeaveRequestViewModel : INotifyPropertyChanged
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
        public NewLeaveRequestViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;


            _windowService = windowService;

            // Initialize commands
            SubmitCommand = new RelayCommand<object>(OnSubmit);
            
            //CloseCommand = new RelayCommand<object>(Close);
        }
        private ObservableCollection<string> _items;
        private string _selectedItem;

        public ObservableCollection<string> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public string SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        private ObservableCollection<string> _items2;
        private string _selectedItem2;

        public ObservableCollection<string> Items2
        {
            get => _items2;
            set => SetProperty(ref _items2, value);
        }

        public string SelectedItem2
        {
            get => _selectedItem2;
            set => SetProperty(ref _selectedItem2, value);
        }
        private DateOnly _startDate;

        public DateOnly StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        private DateOnly _endDate;

        public DateOnly EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }
        public ICommand SubmitCommand { get; }
        private void OnSubmit(object parameter)
        {
        
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
