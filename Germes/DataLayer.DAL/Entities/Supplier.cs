using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer.DAL.Entities
{
    public class Supplier
    {
        [Key]
        [HiddenInput]
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "Input Name!")]
        [Display(Name = "Short Name")]
        public string ShortNameSupplier { get; set; }

        [Required(ErrorMessage = "Input Name!")]
        [Display(Name = "Name")]
        public string NameSupplier { get; set; }

        [Required(ErrorMessage = "Input business form!")]
        [Display(Name = "Business form")]
        public string FormOfBusiness { get; set; }

        public string UNP { get; set; }

        public string RS { get; set; }

        public string Country { get; set; }

        public string LegalAddress { get; set; }

        public string PhysicalAddress { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^((8|\+3)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Not a valid Phone number")]
        public string Phone { get; set; }

        public string Email { get; set; }

        public string BankRequisites { get; set; }

        public string BankAddress { get; set; }

        public virtual ICollection<Price> Prices { get; set; }

    }
}
