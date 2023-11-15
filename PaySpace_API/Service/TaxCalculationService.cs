using PaySpace_API.Models;

namespace PaySpace_API.Service;

public class TaxCalculationService : ITaxCalculationService
{
    private readonly ApplicationDbContext _context;

    public TaxCalculationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public decimal CalculateTax(string postalCode, decimal income)
    {
        var taxType = _context.PostalCodeTaxTypes.FirstOrDefault(p => p.PostalCode == postalCode)?.CalculationType;

        decimal tax = taxType switch
        {
            PostalCodeTaxType.TaxType.Progressive => CalculateProgressiveTax(income),
            PostalCodeTaxType.TaxType.FlatValue => CalculateFlatValueTax(income),
            PostalCodeTaxType.TaxType.FlatRate => CalculateFlatRateTax(income),
            _ => 0m
        };

        SaveTaxCalculationData(postalCode, income, tax);

        return tax;
    }

    public decimal CalculateProgressiveTax(decimal income)
    {
        var tax = 0m;
        var taxRates = _context.TaxRates.OrderBy(tr => tr.FromIncome).ToList();

        foreach (var rate in taxRates)
            if (income > rate.FromIncome)
            {
                var taxableIncome = Math.Min(income, rate.ToIncome) - rate.FromIncome;
                tax += taxableIncome * rate.Rate;
            }
            else
            {
                break;
            }

        return tax;
    }

    public decimal CalculateFlatValueTax(decimal income)
    {
        var rule = _context.FlatValueTaxRules.FirstOrDefault();
        if (rule != null)
            return income < rule.IncomeThreshold ? income * rule.TaxRateBelowThreshold : rule.FlatTaxAmount;
        return 0m;
    }

    public decimal CalculateFlatRateTax(decimal income)
    {
        var rule = _context.FlatRateTaxRules.FirstOrDefault();
        return rule != null ? income * rule.TaxRate : 0m;
    }

    private void SaveTaxCalculationData(string postalCode, decimal income, decimal tax)
    {
        var taxCalculation = new TaxCalculation
        {
            PostalCode = postalCode,
            AnnualIncome = income,
            CalculatedTax = tax,
            CalculationDate = DateTime.Now
        };

        _context.TaxCalculations.Add(taxCalculation);
        _context.SaveChanges();
    }
}