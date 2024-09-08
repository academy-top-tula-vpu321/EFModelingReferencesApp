using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFModelingReferencesApp
{
    public class CompaniesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source=Y5-0\MSSQLSERVER01;Initial Catalog=CompaniesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Company)
                        .WithMany(c => c.Employees)
                        .HasForeignKey(e => e.CompanyId)
                        .OnDelete(DeleteBehavior.SetNull);
                        

            //modelBuilder.Entity<Employee>()
            //            .HasOne(e => e.Company)
            //            .WithMany(c => c.Employees)
            //            .HasForeignKey(e => e.CompanyTitle)
            //            .HasPrincipalKey(c => c.Title);

            //modelBuilder.Entity<Employee>()
            //            .Property(e => e.Company)
            //            .IsRequired();
        }
    }
}
