using System.ComponentModel.DataAnnotations.Schema;

namespace PreInfoTrans.Models
{
    public class Brands
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? BrandName { get; set; }
        public string? BrandCode { get; set; }
    }
}
