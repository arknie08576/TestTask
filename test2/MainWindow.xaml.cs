using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using test2.Models;

namespace test2
{


    public partial class MainWindow : Window
    {
        public OfficeContex context;
        private readonly IWindowService windowService;

        public MainWindow(OfficeContex officeContex, IWindowService _windowService)
        {
            InitializeComponent();
            windowService = _windowService;
            context = officeContex;

        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (AuthenticationHelper.AuthenticateUser(username, password))
            {
                Position role = Position.Employee;
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


                var partner = context.Employes.Where(x => x.Username == username).Select(x => x.Position).ToList();
                
                role = partner[0];





                switch (role)
                {
                    case Position.Employee:
                        
                        windowService.ShowWindow<EmployeeWindow>();
                        
                        break;
                    case Position.HRManager:
                        
                        windowService.ShowWindow<HRManagerWindow>();
                        break;
                    case Position.ProjectManager:
                       
                        windowService.ShowWindow<ProjectManagerWindow>();
                        break;
                    case Position.Administrator:
                       
                        windowService.ShowWindow<AdministratorWindow>();
                        break;



                }


               

                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            
            windowService.ShowWindow<RegisterWindow>();

        }
    }
}