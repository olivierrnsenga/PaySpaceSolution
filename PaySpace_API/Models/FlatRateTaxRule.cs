using System.ComponentModel.DataAnnotations.Schema;

namespace PaySpace_API.Models
{
    public class FlatRateTaxRule
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public decimal TaxRate { get; set; }
    }
}