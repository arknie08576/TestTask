using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{

    public interface IOpenLeaveRequestWindowFactory
    {
        OpenLeaveRequestWindow Create(int id);
    }
    public class OpenLeaveRequestWindowFactory : IOpenLeaveRequestWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OpenLeaveRequestWindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public OpenLeaveRequestWindow Create(int id)
        {
            
            var dbContext = _serviceProvider.GetRequiredService<OfficeContex>();

            
            return new OpenLeaveRequestWindow(id, dbContext);
        }
    }
}
