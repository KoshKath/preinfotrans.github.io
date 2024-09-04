using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreInfoTrans.Models
{
    public class Carrier
    {
        // Лицо ответственное за перевозку
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [DisplayName("Наименование")]
        public string? Name { get; set; }
        
        [DisplayName("УНП")]
        [MinLength(9, ErrorMessage = "Длина УНП должна быть 9 символов")]
        [MaxLength(9, ErrorMessage = "Длина УНП должна быть 9 символов")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Учетный номер плательщика может содержать только цифры")]
        [RequiredIfCountryIsBelarus("Country")]
        public string? Unp { get; set; }
        
        [DisplayName("Страна")]
        [Required]
        public string? Country { get; set; }
        [DisplayName("Адрес")]
        [Required(ErrorMessage ="Адрес обязателен")]
        public string? Address { get; set; }
        [Required]
        public int EpiId { get; set; }
        [DisplayName("Лицо, осуществляющее перевозку товаров")]
        public string GetAllFieldsAsString()
        {
            string name = Name ?? "";
            string unp = Unp ?? "";
            string country = Country ?? "";
            string address = Address ?? "";
            return $"{name}, УНП:{unp}, Адрес: {country}, {address}"; 
        }
    }

    internal class RequiredIfCountryIsBelarusAttribute : ValidationAttribute
    {
        private readonly string _countryPropertyName;

        public RequiredIfCountryIsBelarusAttribute(string countryPropertyName)
        {
            _countryPropertyName = countryPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var countryProperty = validationContext.ObjectType.GetProperty(_countryPropertyName);
            if (countryProperty == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var countryValue = countryProperty.GetValue(validationContext.ObjectInstance)?.ToString();

            if (countryValue == "БЕЛАРУСЬ" && string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult($"Необходимо указать {validationContext.DisplayName}.");
            }

            return ValidationResult.Success;
        }
    }
}
