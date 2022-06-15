using System.ComponentModel.DataAnnotations;

namespace RCARS.Interface.Models
{
    public class Vehicle
    {
        [Key]
        public string NumberPlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int PurchaseKms { get; set; }
        public string Fuel { get; set; }
        public string Transmission { get; set; }
        public DateTime InspectionDate { get; set; }
        public string TitleOwner { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool SecondKey { get; set; }
        public bool Manual { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal BodyworkCost { get; set; }
        public decimal CleaningCost { get; set; }
        public decimal TotalCost { get; }

    }
}
