using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace PreInfoTrans.Models
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }
        //[DisplayName("Пароль")]
        //public string Password { get; set; }
        [DisplayName("Адрес электронной почты")]
        public string Email { get; set; }
        [DisplayName("Роль")]
        public string RoleId { get; set; }

        public SelectList? Roles { get; set; }
    }
}
