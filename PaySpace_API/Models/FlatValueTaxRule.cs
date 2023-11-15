namespace PaySpace_API.Models
{
    public class FlatValueTaxRule
    {
        public int Id { get; set; }
        public decimal IncomeThreshold { get; set; } 
        public decimal FlatTaxAmount { get; set; } 
        public decimal TaxRateBelowThreshold { get; set; } 
    }
}