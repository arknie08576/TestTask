using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace test2.Behaviors
{
    public static class DoubleClickBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(DoubleClickBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid)
            {
                dataGrid.LoadingRow -= OnLoadingRow;
                dataGrid.LoadingRow += OnLoadingRow;
            }
        }

        private static void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.MouseDoubleClick -= OnMouseDoubleClick;
            e.Row.MouseDoubleClick += OnMouseDoubleClick;
        }

        private static void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row)
            {
                var command = GetCommand(row);
                if (command != null && command.CanExecute(row.DataContext))
                {
                    command.Execute(row.DataContext);
                }
            }
        }
    }
}
