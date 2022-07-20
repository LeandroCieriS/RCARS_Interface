using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCARS.Interface.Models
{
    public class Purchase
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal PurchasePrice { get; set; }

        [Column("Customer")]
        public int CustomerId { get; set; }

        [Column("Vehicle")]
        public string NumberPlate { get; set; }
    }
}
