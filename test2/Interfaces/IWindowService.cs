

namespace test2.Interfaces
{
    public interface IWindowService
    {
        void ShowWindow<TViewModel>(object parameter = null) where TViewModel : class;
        void CloseWindow<TViewModel>() where TViewModel : class;
        void ShowDialog<TViewModel>(object parameter = null) where TViewModel : class;
    }
}
