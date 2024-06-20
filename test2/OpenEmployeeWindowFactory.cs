using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{

    public interface IOpenEmployeeWindowFactory
    {
        OpenEmployeeWindow Create(int id);
    }
    public class OpenEmployeeWindowFactory : IOpenEmployeeWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OpenEmployeeWindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public OpenEmployeeWindow Create(int id)
        {
            // Retrieve required services from the service provider
            var dbContext = _serviceProvider.GetRequiredService<OfficeContex>();

            // Create the window with required parameters
            return new OpenEmployeeWindow(id, dbContext);
        }
    }
}
