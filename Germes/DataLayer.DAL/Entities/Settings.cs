using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer.DAL.Entities
{

    public class Settings
    {
        [Key]
        [HiddenInput]
        public int SettingID { get; set; }
        
        public double Currency { get; set; }

        public double Markup { get; set; }

        [Required(ErrorMessage = "Input Name!")]
        [Display(Name = "Short Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Input business form!")]
        [Display(Name = "Business form")]
        public string FormOfBusiness { get; set; }

        public string UNP { get; set; }

        public string RS { get; set; }

        public string Country { get; set; }

        public string LegalAddress { get; set; }

        public string PhysicalAddress { get; set; }

        public string BankRequisites { get; set; }

        public string BankAddress { get; set; }

    }
}
