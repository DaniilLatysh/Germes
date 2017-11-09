using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DAL.Entities
{
    public class Order
    {
        [Key]
        [HiddenInput]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public virtual Client Client { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<CartItem> Products { get; set; }

        public double? TotalAmount { get; set; }
 
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreateOrder { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "From")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? DeliveryTimeFrom { get; set; }


        [Display(Name = "To")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? DeliveryTimeTo { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CloseOrder { get; set; }

        [Display(Name = "Shipping")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^([1-9]{1}[\d]{0,2}(\,[\d]{3})*(\.[\d]{0,2})?|[1-9]{1}[\d]{0,}(\.[\d]{0,2})?|0(\.[\d]{0,2})?|(\.[\d]{1,2})?)$", ErrorMessage = "Not a valid value")]
        public float? CostDelivery { get; set; }
    }

    public class Client
    {
        [Key]
        [HiddenInput]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }

        [Display(Name = "Name")]
        public string NameClient { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^((8|\+3)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        public string Adress { get; set; }

        public string Mood { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }


    public class Status
    {
        [Key]
        [HiddenInput]
        public int StatusID { get; set; }

        public string NameStatus { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

