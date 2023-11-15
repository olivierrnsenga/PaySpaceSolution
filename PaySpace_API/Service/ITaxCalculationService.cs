namespace PaySpace_API.Service
{
    public interface ITaxCalculationService
    {
        decimal CalculateTax(string taxType, decimal annualIncome);
    }
}