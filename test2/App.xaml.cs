using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Windows;
using System.Windows.Forms.Design;
using Microsoft.EntityFrameworkCore;
using System.IO;
using test2.ViewModels;
using test2.Views;
using test2.Helpers;
using test2.Services;
using test2.Interfaces;
using test2.Data;



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
            services.AddTransient<ChangePasswordWindow>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<EmployeeViewModel>();
            services.AddTransient<HRManagerViewModel>();
            services.AddTransient<ProjectManagerViewModel>();
            services.AddTransient<AdministratorViewModel>();
            services.AddTransient<LeaveRequestsViewModel>();
            services.AddTransient<NewLeaveRequestViewModel>();
            services.AddTransient<EditLeaveRequestViewModel>();
            services.AddTransient<ProjectsViewModel>();
            services.AddTransient<NewProjectViewModel>();
            services.AddTransient<EditProjectViewModel>();
            services.AddTransient<OpenProjectViewModel>();
            services.AddTransient<EmployesViewModel>();
            services.AddTransient<OpenEmployeeViewModel>();
            services.AddTransient<ApprovalRequestsViewModel>();
            services.AddTransient<OpenApprovalRequestViewModel>();
            services.AddTransient<ChangePasswordViewModel>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddDbContext<OfficeContex>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


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
