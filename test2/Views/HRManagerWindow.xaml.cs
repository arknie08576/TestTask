using System.Windows;
using test2.Services;
using test2.Interfaces;

namespace test2.Views
{
    public partial class HRManagerWindow : Window
    {   
        public HRManagerWindow( IWindowService _windowService)
        {
            InitializeComponent();         
        }
    }
}
