using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{

    public interface IEditLeaveRequestWindowFactory
    {
        EditLeaveRequestWindow Create(int id);
    }
    public class EditLeaveRequestWindowFactory : IEditLeaveRequestWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public EditLeaveRequestWindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public EditLeaveRequestWindow Create(int id)
        {
            
            var dbContext = _serviceProvider.GetRequiredService<OfficeContex>();

            
            return new EditLeaveRequestWindow(id, dbContext);
        }
    }
}
