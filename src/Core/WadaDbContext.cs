using Core.Models;
using Core.Models.EmployeeDomain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

namespace Core
{
    public class WadaDbContext: DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();

		public DbSet<DepartmentEntity> Departments => Set<DepartmentEntity>();

		public WadaDbContext(DbContextOptions<WadaDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

#if DEBUG
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
#endif
        }
    }
}
