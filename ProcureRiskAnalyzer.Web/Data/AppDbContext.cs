using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Models;

namespace ProcureRiskAnalyzer.Web.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Tender> Tenders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Microsoft", Country = "USA" },
                new Supplier { Id = 2, Name = "EPAM", Country = "Ukraine" },
                new Supplier { Id = 3, Name = "SoftServe", Country = "Poland" }
            );

            modelBuilder.Entity<Tender>().HasData(
                new Tender { Id = 1, TenderCode = "T001", Buyer = "Міністерство освіти", Date = new DateTime(2025, 10, 10), ExpectedValue = 1000000, SupplierId = 1 },
                new Tender { Id = 2, TenderCode = "T002", Buyer = "Міністерство охорони здоров’я", Date = new DateTime(2025, 10, 15), ExpectedValue = 750000, SupplierId = 2 }
            );
        }
    }
}
