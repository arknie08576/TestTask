using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace test2
{
    public interface IWindowService
    {
        void ShowWindow<T>(params object[] args) where T : Window;
    }
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowWindow<T>(params object[] args) where T : Window
        {

            T window;

            if (typeof(T) == typeof(OpenProjectWindow) && args.Length == 1 && args[0] is int id)
            {
                var factory = _serviceProvider.GetRequiredService<IOpenProjectWindowFactory>();
                window = (T)(object)factory.Create(id);
            }
            else
            {
                window = _serviceProvider.GetRequiredService<T>();
            }

            window.Show();
        }
    }
}
