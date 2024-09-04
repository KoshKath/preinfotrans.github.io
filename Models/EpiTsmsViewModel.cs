using PreInfoTrans.Controllers;
using System.ComponentModel.DataAnnotations;
namespace PreInfoTrans.Models
{
    public class EpiTsmsViewModel
    {
        public string EpiDocName { get; set; }
        public Epi Epi {  get; set; }
        public Carrier? Carrier { get; set; }
        public Owner? Owner { get; set; }
        [Required, MinLength(1)]
        public List<Tsmp>? Tsmps { get; set; }
        public List<Countries>? Countries { get; set; }
        public bool IsValidTsmp() 
            {
                return Tsmps != null && Tsmps.Any();
            }

    }
    
}
