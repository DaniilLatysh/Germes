using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer.DAL.Entities
{
    public class Price
    {
        [Key]
        [HiddenInput]
        public int PriceId { get; set; }

        public string ProductKey { get; set; }

        public string ProductName { get; set; }

        public string Warranty { get; set; }

        public string Quantity { get; set; }
 
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Input Price")]
        [Range(minimum: 0.00, maximum: 99999.99)]
        public double? PriceIn { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Input Price")]
        [Range(minimum: 0.00, maximum: 99999.99)]
        public double? PriceSale { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
