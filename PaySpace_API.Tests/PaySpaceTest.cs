using Microsoft.EntityFrameworkCore;
using PaySpace_API.Models;
using PaySpace_API.Service;

namespace PaySpace_API.Tests;

public class PaySpaceTest
{
    private ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("DB-DEV-PAYSPACE")
            .Options;

        var context = new ApplicationDbContext(options);

        if (!context.FlatRateTaxRules.Any())
        {
            context.FlatRateTaxRules.Add(new FlatRateTaxRule { Id = 1, TaxRate = 0.175M });
            context.SaveChanges();
        }

        if (!context.TaxRates.Any())
        {
            context.TaxRates.AddRange(
                new TaxRate { Id = 1, Rate = 0.10M, FromIncome = 0, ToIncome = 8350 },
                new TaxRate { Id = 2, Rate = 0.15M, FromIncome = 8351, ToIncome = 33950 },
                new TaxRate { Id = 3, Rate = 0.25M, FromIncome = 33951, ToIncome = 82250 },
                new TaxRate { Id = 4, Rate = 0.28M, FromIncome = 82251, ToIncome = 171550 },
                new TaxRate { Id = 5, Rate = 0.33M, FromIncome = 171551, ToIncome = 372950 },
                new TaxRate { Id = 6, Rate = 0.35M, FromIncome = 372951, ToIncome = decimal.MaxValue }
            );
            context.SaveChanges();
        }

        if (!context.FlatValueTaxRules.Any())
        {
            context.FlatValueTaxRules.Add(
                new FlatValueTaxRule
                    { Id = 1, IncomeThreshold = 200000, FlatTaxAmount = 10000, TaxRateBelowThreshold = 0.05M }
            );
            context.SaveChanges();
        }

        return context;
    }

    [Fact]
    public void CalculateFlatRateTax_ShouldReturnCorrectTax()
    {
        using var context = CreateDbContext();
        var service = new TaxCalculationService(context);
        decimal income = 100000;
        var expectedTax = income * 0.175M;

        // Act
        var actualTax = service.CalculateFlatRateTax(income);

        // Assert
        Assert.Equal(expectedTax, actualTax);
    }

    [Fact]
    public void CalculateProgressiveTax_ShouldCalculateCorrectly_ForLowerBracket()
    {
        // Arrange
        using var context = CreateDbContext();
        var service = new TaxCalculationService(context);
        decimal income = 5000;
        var expectedTax = income * 0.10M;

        // Act
        var actualTax = service.CalculateProgressiveTax(income);

        // Assert
        Assert.Equal(expectedTax, actualTax);
    }

    [Fact]
    public void CalculateFlatValueTax_ShouldCalculateFivePercent_ForIncomeBelowThreshold()
    {
        // Arrange
        using var context = CreateDbContext();
        var service = new TaxCalculationService(context);
        decimal income = 150000;
        var expectedTax = income * 0.05M;

        // Act
        var actualTax = service.CalculateFlatValueTax(income);

        // Assert
        Assert.Equal(expectedTax, actualTax);
    }

    [Fact]
    public void CalculateFlatValueTax_ShouldReturnFlatAmount_ForIncomeAboveThreshold()
    {
        // Arrange
        using var context = CreateDbContext();
        var service = new TaxCalculationService(context);
        decimal income = 250000;
        var expectedTax = 10000M;

        // Act
        var actualTax = service.CalculateFlatValueTax(income);

        // Assert
        Assert.Equal(expectedTax, actualTax);
    }

    [Fact]
    public void CalculateFlatRateTax_ShouldReturnZero_ForZeroIncome()
    {
        // Arrange
        using var context = CreateDbContext();
        var service = new TaxCalculationService(context);
        decimal income = 0;
        decimal expectedTax = 0;

        // Act
        var actualTax = service.CalculateFlatRateTax(income);

        // Assert
        Assert.Equal(expectedTax, actualTax);
    }
}