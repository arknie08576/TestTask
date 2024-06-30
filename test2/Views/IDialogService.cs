using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace test2.Views
{
    public interface IDialogService
    {
        void ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon);
    }
}
