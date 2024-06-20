using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{

    public interface IOpenApprovalRequestWindowFactory
    {
        OpenApprovalRequestWindow Create(int id);
    }
    public class OpenApprovalRequestWindowFactory : IOpenApprovalRequestWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OpenApprovalRequestWindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public OpenApprovalRequestWindow Create(int id)
        {
            
            var dbContext = _serviceProvider.GetRequiredService<OfficeContex>();

            
            return new OpenApprovalRequestWindow(id, dbContext);
        }
    }
}
