﻿using Microsoft.Extensions.DependencyInjection;
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
        void ShowWindow<TViewModel>() where TViewModel : class;
        void CloseWindow<TViewModel>() where TViewModel : class;
        void ShowDialog<TViewModel>() where TViewModel : class;
    }
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, Window> _openWindows = new Dictionary<Type, Window>();

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowWindow<TViewModel>() where TViewModel : class
        {
            var viewModelType = typeof(TViewModel);
            if (_openWindows.ContainsKey(viewModelType))
            {
                _openWindows[viewModelType].Activate();
                return;
            }

            var window = CreateWindow(viewModelType);
            if (window != null)
            {
                _openWindows[viewModelType] = window;
                window.Closed += (s, e) => _openWindows.Remove(viewModelType);
                window.Show();
            }
        }

        public void ShowDialog<TViewModel>() where TViewModel : class
        {
            var viewModelType = typeof(TViewModel);
            var window = CreateWindow(viewModelType);
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

        private Window CreateWindow(Type viewModelType)
        {
            var windowType = GetWindowTypeForViewModel(viewModelType);
            if (windowType == null) return null;

            var window = (Window)_serviceProvider.GetService(windowType);
            var viewModel = _serviceProvider.GetService(viewModelType);
            window.DataContext = viewModel;
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
