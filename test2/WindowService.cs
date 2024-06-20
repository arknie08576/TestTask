using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            var window = _serviceProvider.GetRequiredService<T>();
            if (args.Length > 0)
            {
                var constructors = typeof(T).GetConstructors();
                var constructor = constructors.FirstOrDefault(c => c.GetParameters().Length == args.Length);
                if (constructor != null)
                {
                    window = (T)constructor.Invoke(args);
                }
                else
                {
                    throw new InvalidOperationException($"No constructor found for window type {typeof(T)} with the provided parameters.");
                }
            }
            window.Show();
        }
    }
}
