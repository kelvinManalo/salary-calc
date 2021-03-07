using Microsoft.EntityFrameworkCore;
using salary_calc_api.Models;

namespace salary_calc_api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees {get;set;}

        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().ToTable("Employee");
            builder.Entity<Employee>().HasKey(p => p.employeeId);
            builder.Entity<Employee>().Property(p => p.employeeId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(p => p.name).IsRequired();
            builder.Entity<Employee>().Property(p => p.birthdate).IsRequired();
            builder.Entity<Employee>().Property(p => p.tin).IsRequired();
            builder.Entity<Employee>().Property(p => p.employeeType).IsRequired();
            builder.Entity<Employee>().Property(p => p.baseSalary).IsRequired();
            builder.Entity<Employee>().Property(p => p.effectiveDays);
            builder.Entity<Employee>().Property(p => p.computedSalary);
            builder.Entity<Employee>().Property(p => p.completed);
        }
    }
}