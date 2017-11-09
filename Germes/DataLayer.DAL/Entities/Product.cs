using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DataLayer.DAL.Entities
{
    public class Product
    {
        [Key]
        [HiddenInput]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "Input manufacturer!")]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Input model!")]
        [Display(Name = "Model name")]
        public string Model { get; set; }

        [Display(Name = "Extended model")]
        public string ExtendedModel { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [DefaultValue(0)]
        public int Rating { get; set; }
        
        public virtual ICollection<CartItem> Orders { get; set; }
        public virtual ICollection<Price> Price { get; set; }

        public virtual Price CurrentPrice { get; set; }

        public string Warranty { get; set; }

        public int Quantity { get; set; }

        public double? PriceIn { get; set; }

        public double? PriceSale { get; set; }


        public override string ToString()
        {
            string result = Manufacturer + " " + Model + " " + ExtendedModel + " " + Description + " " + Color;
            return result.ToUpper();
        }
    }

    public class Category
    {
        [Key]
        [HiddenInput]
        public int CategoryID { get; set; }

        public int ParentCategoryID { get; set; }

        public string NameCayegory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
