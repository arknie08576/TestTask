using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Forms.Design;
using Microsoft.EntityFrameworkCore;

namespace test2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register services and view models
            services.AddTransient<MainWindow>();
            services.AddTransient<RegisterWindow>();
            services.AddDbContext<OfficeContex>(options =>
            options.UseSqlServer("Data Source=DESKTOP-TEFRQV5\\SQLEXPRESS;Initial Catalog=Out_of_Office;Integrated Security=True;Encrypt=False"));
            //services.AddTransient<IMyService, MyService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }





    }

}
