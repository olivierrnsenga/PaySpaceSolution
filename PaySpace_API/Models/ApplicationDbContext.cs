using Microsoft.EntityFrameworkCore;

namespace PaySpace_API.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor accepting DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<TaxCalculation> TaxCalculations { get; set; }
        public DbSet<PostalCodeTaxType> PostalCodeTaxTypes { get; set; }
        public DbSet<FlatValueTaxRule> FlatValueTaxRules { get; set; }
        public DbSet<FlatRateTaxRule> FlatRateTaxRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for PostalCodeTaxType
            modelBuilder.Entity<PostalCodeTaxType>().HasData(
                new PostalCodeTaxType { Id = 1, PostalCode = "7441", CalculationType = PostalCodeTaxType.TaxType.Progressive },
                new PostalCodeTaxType { Id = 2, PostalCode = "A100", CalculationType = PostalCodeTaxType.TaxType.FlatValue },
                new PostalCodeTaxType { Id = 3, PostalCode = "7000", CalculationType = PostalCodeTaxType.TaxType.FlatRate },
                new PostalCodeTaxType { Id = 4, PostalCode = "1000", CalculationType = PostalCodeTaxType.TaxType.Progressive }
            );

            // Seed data for progressive TaxRate
            modelBuilder.Entity<TaxRate>().HasData(
                new TaxRate { Id = 1, Rate = 0.10M, FromIncome = 0, ToIncome = 8350 },
                new TaxRate { Id = 2, Rate = 0.15M, FromIncome = 8351, ToIncome = 33950 },
                new TaxRate { Id = 3, Rate = 0.25M, FromIncome = 33951, ToIncome = 82250 },
                new TaxRate { Id = 4, Rate = 0.28M, FromIncome = 82251, ToIncome = 171550 },
                new TaxRate { Id = 5, Rate = 0.33M, FromIncome = 171551, ToIncome = 372950 },
                new TaxRate { Id = 6, Rate = 0.35M, FromIncome = 372951, ToIncome = 100000000 }
            );

            // Seed data for FlatValueTaxRule
            modelBuilder.Entity<FlatValueTaxRule>().HasData(
                new FlatValueTaxRule { Id = 1, IncomeThreshold = 200000, FlatTaxAmount = 10000, TaxRateBelowThreshold = 0.05M }
            );

            // Seed data for FlatRateTaxRule
            modelBuilder.Entity<FlatRateTaxRule>().HasData(
                new FlatRateTaxRule { Id = 1, TaxRate = 0.175M }
            );
        }
    }
}