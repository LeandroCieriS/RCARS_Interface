using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCARS.Interface.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal SoldPrice { get; set; }

        [Column("Customer")]
        public int CustomerId { get; set; }

        [Column("Vehicle")]
        public string NumberPlate { get; set; }
        public int WarrantyMonths { get; set; }
    }
}
