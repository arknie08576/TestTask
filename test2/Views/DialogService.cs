using System.Windows;
using test2.Interfaces;

namespace test2.Views
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBox.Show(message, caption, button, icon);
        }
    }
}
