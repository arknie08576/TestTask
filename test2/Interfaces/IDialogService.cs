using System.Windows;

namespace test2.Interfaces
{
    public interface IDialogService
    {
        void ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon);
    }
}
