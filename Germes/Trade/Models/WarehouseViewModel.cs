using DataLayer.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Trade.Models
{
    public class WarehouseViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Price> Price { get; set; }
        [Display(Name = "Status")]
        public int SelectedCategoryID { get; set; }
        public IEnumerable<SelectListItem> CategoriesSLI { get; set; }
    }
}