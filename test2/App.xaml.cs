using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Forms.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using OutofOffice.ViewModels;
using test2.ViewModels;
using test2.View;
using Prism.Events;
using OutofOffice;


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
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<EmployeeViewModel>();
            services.AddTransient<HRManagerViewModel>();
            services.AddTransient<ProjectManagerViewModel>();
            services.AddTransient<AdministratorViewModel>();
            services.AddTransient<LeaveRequestsViewModel>();
            services.AddTransient<NewLeaveRequestViewModel>();
            
            services.AddSingleton<IDialogService, DialogService>();
           // services.AddSingleton<IEventAggregator, EventAggregator>();

            services.AddDbContext<OfficeContex>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddTransient<IMyService, MyService>();
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
               var service = _serviceProvider.GetService<IWindowService>();
            AuthenticationHelper.LoadDbContext(_serviceProvider.GetRequiredService<OfficeContex>());
            service.ShowWindow<MainViewModel>();
        }





    }

}
