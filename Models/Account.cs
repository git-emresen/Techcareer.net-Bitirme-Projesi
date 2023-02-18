using System.ComponentModel.DataAnnotations;

namespace Bitirme_Projesi.Models
{
    public class Account
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Key]
        public string Email { get; set; }
		[Required] 
        [MinLength(8)]
		[RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}")]
		public string Password { get; set; }
        
		[Required]
        [MinLength(8)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}")]    
		public string PasswordRepeat { get; set; }   

    }
}
