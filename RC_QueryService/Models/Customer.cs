using System.ComponentModel.DataAnnotations;

namespace RCARS.Interface.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public bool IsCompany { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
