using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{/*
    public interface IOpenProjectWindowFactory
    {
        OpenProjectWindow Create(int id);
    }
    public class OpenProjectWindowFactory : IOpenProjectWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OpenProjectWindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public OpenProjectWindow Create(int id)
        {
            
            var dbContext = _serviceProvider.GetRequiredService<OfficeContex>();

            
            return new OpenProjectWindow(id, dbContext);
        }
    }*/
}
