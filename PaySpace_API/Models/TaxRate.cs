namespace PaySpace_API.Models
{
    public class TaxRate
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public decimal FromIncome { get; set; }
        public decimal ToIncome { get; set; }
    }
}