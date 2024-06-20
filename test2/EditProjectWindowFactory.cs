using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    public interface IEditProjectWindowFactory
    {
        EditProjectWindow Create(int id);
    }
    public class EditProjectWindowFactory : IEditProjectWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public EditProjectWindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public EditProjectWindow Create(int id)
        {
            // Retrieve required services from the service provider
            var dbContext = _serviceProvider.GetRequiredService<OfficeContex>();

            // Create the window with required parameters
            return new EditProjectWindow(id, dbContext);
        }
    }
}
