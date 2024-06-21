using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace test2
{
    public class OfficeContexFactory : IDesignTimeDbContextFactory<OfficeContex>
    {
        public OfficeContex CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OfficeContex>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-TEFRQV5\\SQLEXPRESS;Initial Catalog=Out_of_Office2;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
            return new OfficeContex(optionsBuilder.Options);
        }

    }
}
