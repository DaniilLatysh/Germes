using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DAL.Entities
{
    public class CartItem
    {
        [Key]
        [HiddenInput]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }

        [Range(minimum:0, maximum: 999)]
        public int Quantity { get; set; }

        [Range(minimum: 0, maximum: 99999)]
        public double? PriceIn { get; set; }

        [Range(minimum: 0, maximum: 99999)]
        public double? PriceSale { get; set; }
    }
}
