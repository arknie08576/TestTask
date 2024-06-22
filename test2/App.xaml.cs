using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Forms.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace test2
{
    
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
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

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
            services.AddTransient<EditLeaveRequestWindow>();
            services.AddTransient<OpenProjectWindow>();
            services.AddTransient<IOpenProjectWindowFactory, OpenProjectWindowFactory>();
            services.AddTransient<IEditProjectWindowFactory, EditProjectWindowFactory>();
            services.AddTransient<IOpenApprovalRequestWindowFactory, OpenApprovalRequestWindowFactory>();
            services.AddTransient<IOpenEmployeeWindowFactory, OpenEmployeeWindowFactory>();
            services.AddTransient<IEditLeaveRequestWindowFactory, EditLeaveRequestWindowFactory>();


            services.AddDbContext<OfficeContex>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddTransient<IMyService, MyService>();
        }
        private IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
               var mainWindow = _serviceProvider.GetService<MainWindow>();
            AuthenticationHelper.LoadDbContext(_serviceProvider.GetRequiredService<OfficeContex>());
            mainWindow.Show();
        }





    }

}
