using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreInfoTrans.Models
{
    public class TsmpTypes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public int TypeCode { get; set; }
        [Required]
        public string? TypeName { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
