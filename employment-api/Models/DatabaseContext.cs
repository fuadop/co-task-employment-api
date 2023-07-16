using System;
using Microsoft.EntityFrameworkCore;

namespace employment_api.Models
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options)
			:base(options)
		{
			
		}

		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Employee>()
				.Property(b => b.ID)
				.HasDefaultValueSql("NEWID()");

            builder.Entity<Employee>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            builder.Entity<Employee>()
                .Property(b => b.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Department>()
                .Property(b => b.ID)
                .HasDefaultValueSql("NEWID()");
            builder.Entity<Department>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            builder.Entity<Employee>()
                .Property(b => b.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Department>()
               .HasIndex(b => b.Code)
               .IsUnique();
        }

        public override int SaveChanges()
        {
            var updatedEmployees = ChangeTracker.Entries<Employee>()
                   .Where(o => o.State == EntityState.Modified);
            foreach (var employee in updatedEmployees)
            {
                employee.Entity.UpdatedAt = DateTime.Now;
            }

            var updatedDepartments = ChangeTracker.Entries<Department>()
                   .Where(o => o.State == EntityState.Modified);
            foreach (var department in updatedDepartments)
            {
                department.Entity.UpdatedAt = DateTime.Now;
            }

            return base.SaveChanges();
        }
	}
}

