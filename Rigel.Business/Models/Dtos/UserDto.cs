using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rigel.Business.Models.Dtos
{
    public class UserDto : BaseDto
    {
        [MaxLength(10,ErrorMessage ="Kullaıcı adı 10 karakterden fazla olamaz.")]
        public string UserName { get; set; }


        [MaxLength(10, ErrorMessage = "Şifre 100 karakterden fazla olamaz.")]
        public string Password { get; set; }

        [MaxLength(50,ErrorMessage ="Email 50 karakterden fazla olmaz.")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Geçerli bir email adresi değil.")]
        public string Email { get; set; }

        public DateTime LastActivity { get; set; }

        public ICollection<TodoDto> Todoes { get; set; }

    }
}