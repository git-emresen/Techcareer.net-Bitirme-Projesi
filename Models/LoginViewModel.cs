using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bitirme_Projesi.Models
{
    
        public class LoginViewModel
        {
            [Display(Name = "Kullanıcı Adı", Prompt = "Kulanici Adını Gir")] //label ve placeholder(htmlde yazılmamışsa)
            [Required(ErrorMessage = "Username is required")]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Password is required")]           
            public string Password { get; set; }
        }
    
}
