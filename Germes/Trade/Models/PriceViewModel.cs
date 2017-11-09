using DataLayer.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Trade.Models
{
    public class PriceViewModel
    {
        public IEnumerable<Price> Price { get; set;}
        public IEnumerable<Supplier> Suppliers { get; set; }
        [Display(Name = "Supplier")]
        public int SelectedSupplierID { get; set; }
        public IEnumerable<SelectListItem> SuppliersSLI { get; set; }
    }
}