using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2.Interfaces
{
    public interface IWindowService
    {
        void ShowWindow<TViewModel>(object parameter = null) where TViewModel : class;
        void CloseWindow<TViewModel>() where TViewModel : class;
        void ShowDialog<TViewModel>(object parameter = null) where TViewModel : class;
    }
}
