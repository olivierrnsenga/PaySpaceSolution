namespace PaySpace_API.Models
{
    public class TaxCalculationResponse
    {
        public string PostalCode { get; set; }
        public decimal Income { get; set; }
        public decimal Tax { get; set; }
    }
}