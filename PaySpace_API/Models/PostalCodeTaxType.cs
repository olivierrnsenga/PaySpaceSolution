namespace PaySpace_API.Models
{
    public class PostalCodeTaxType
    {
        public int Id { get; set; }

        public string PostalCode { get; set; }
        public TaxType CalculationType { get; set; }

        public enum TaxType
        {
            Progressive,
            FlatValue,
            FlatRate
        }
    }
}