using System.Windows;
namespace test2
{
    public interface IWindowService
    {
        void ShowWindow<TViewModel>(object parameter = null) where TViewModel : class;
        void CloseWindow<TViewModel>() where TViewModel : class;
        void ShowDialog<TViewModel>(object parameter = null) where TViewModel : class;
    }
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, Window> _openWindows = new Dictionary<Type, Window>();

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowWindow<TViewModel>(object parameter = null) where TViewModel : class
        {
            var viewModelType = typeof(TViewModel);
            if (_openWindows.ContainsKey(viewModelType))
            {
                _openWindows[viewModelType].Activate();
                return;
            }

            var window = CreateWindow(viewModelType, parameter);
            if (window != null)
            {
                _openWindows[viewModelType] = window;
                window.Closed += (s, e) => _openWindows.Remove(viewModelType);
                window.Show();
            }
        }

        public void ShowDialog<TViewModel>(object parameter = null) where TViewModel : class
        {
            var viewModelType = typeof(TViewModel);
            var window = CreateWindow(viewModelType, parameter);
            window?.ShowDialog();
        }

        public void CloseWindow<TViewModel>() where TViewModel : class
        {
            var viewModelType = typeof(TViewModel);
            if (_openWindows.ContainsKey(viewModelType))
            {
                _openWindows[viewModelType].Close();
                _openWindows.Remove(viewModelType);
            }
        }

        private Window CreateWindow(Type viewModelType, object parameter)
        {
            var windowType = GetWindowTypeForViewModel(viewModelType);
            if (windowType == null) return null;

            var window = (Window)_serviceProvider.GetService(windowType);
            var viewModel = _serviceProvider.GetService(viewModelType);
            window.DataContext = viewModel;
            if (viewModel is IParameterReceiver receiver)
            {
                receiver.ReceiveParameter(parameter);
            }
            return window;
        }

        private Type GetWindowTypeForViewModel(Type viewModelType)
        {
            // Find the window type associated with the view model type
            var viewTypeName = viewModelType.Name.Replace("ViewModel", "Window");
            var windowType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == viewTypeName);
            return windowType;
        }
    }
}
