using System.ComponentModel.DataAnnotations.Schema;

namespace PreInfoTrans.Models
{
    public class Countries
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
    }
}
