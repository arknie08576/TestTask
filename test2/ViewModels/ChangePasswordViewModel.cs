using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using test2.Commands;
using test2.Data;
using test2.Helpers;
using test2.Interfaces;

namespace test2.ViewModels
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        private readonly OfficeContex context;
        private readonly IDialogService _dialogService;
        private readonly IWindowService _windowService;
        public ChangePasswordViewModel(OfficeContex officeContex, IDialogService dialogService, IWindowService windowService)
        {
            context = officeContex;
            _dialogService = dialogService;
            _windowService = windowService;

            ChangePasswordCommand = new AsyncRelayCommand<object>(OnChangePasswordAsync);
            
        }
        public ICommand ChangePasswordCommand { get; }
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        private async Task OnChangePasswordAsync(object parameter)
        {
            if (AuthenticationHelper.loggedUser == null)
            {

                _dialogService.ShowMessage("User logged out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _windowService.CloseWindow<ChangePasswordViewModel>();
                return;
            }
            if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))
            {
                if (Password.Length > 100)
                {
                    _dialogService.ShowMessage("Password can't be longer than 100 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                    return;


                }



                if (Password == ConfirmPassword)
                {

                    try
                    {
                        await AuthenticationHelper.ChangePasswordAsync(AuthenticationHelper.loggedUser, Password);
                        _dialogService.ShowMessage("Password Changed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        _windowService.CloseWindow<ChangePasswordViewModel>();
                    }
                    catch (InvalidOperationException ex)
                    {
                        _dialogService.ShowMessage(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                    
                }
                else
                {
                    _dialogService.ShowMessage("Password and Confirm password are not the same.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }



            }
            else
            {
                _dialogService.ShowMessage("Fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }
    }
}
