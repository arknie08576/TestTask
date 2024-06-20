using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Forms.Design;
using Microsoft.EntityFrameworkCore;
using System;

namespace test2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
      //  private readonly IMainWindow mainWindow;

        public App()
        {
            
          //  mainWindow = _mainWindow;
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register services and view models
            services.AddSingleton<IWindowService, WindowService>();
            services.AddTransient<MainWindow>();
            services.AddTransient<RegisterWindow>();
            services.AddTransient<EmployeeWindow>();
            services.AddTransient<HRManagerWindow>();
            services.AddTransient<ProjectManagerWindow>();
            services.AddTransient<AdministratorWindow>();
            services.AddTransient<ProjectsWindow>();
            services.AddTransient<LeaveRequestsWindow>();
            services.AddTransient<EditProjectWindow>();
            services.AddTransient<ApprovalRequestsWindow>();
            services.AddTransient<EmployesWindow>();
            services.AddTransient<NewLeaveRequestWindow>();
            services.AddTransient<NewProjectWindow>();
            services.AddTransient<OpenApprovalRequestWindow>();
            services.AddTransient<OpenEmployeeWindow>();
            services.AddTransient<OpenLeaveRequestWindow>();
            services.AddTransient<OpenProjectWindow>();
            services.AddTransient<IOpenProjectWindowFactory, OpenProjectWindowFactory>();





            services.AddDbContext<OfficeContex>(options =>
            options.UseSqlServer("Data Source=DESKTOP-7GGELFU\\SQLEXPRESS;Initial Catalog=Out_of_Office;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"));
            //services.AddTransient<IMyService, MyService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //serviceProvider.GetRequiredService<IMainWindow>();
               var mainWindow = _serviceProvider.GetService<MainWindow>();
            AuthenticationHelper.LoadDbContext(_serviceProvider.GetRequiredService<OfficeContex>());
            mainWindow.Show();
        }





    }

}
