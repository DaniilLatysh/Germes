namespace Trade.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using DataLayer.DAL.Entities;
    using DataLayer.DAL.Interfaces;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class OrderViewModel
    {

        public Order Order { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<CartItem> Cart { get; set; }

        [Display(Name = "Status")]
        public int SelectedSatusID { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }

    }
}
