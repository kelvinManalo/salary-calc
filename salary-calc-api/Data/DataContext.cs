using Microsoft.EntityFrameworkCore;
using salary_calc_api.Models;

namespace salary_calc_api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees {get;set;}
        public DbSet<RegularEmployee> RegularEmployees {get;set;}
        public DbSet<ContractualEmployee> ContractualEmployees {get;set;}

        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().ToTable("Employee");
            builder.Entity<RegularEmployee>().ToTable("RegularEmployee");
            builder.Entity<ContractualEmployee>().ToTable("ContractualEmployee");
            
        }
    }
}