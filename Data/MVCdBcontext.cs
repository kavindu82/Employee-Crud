using ASP.NET.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.Data
{
    public class MVCdBcontext : DbContext
    {
        public MVCdBcontext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }

}
