using Bitirme_Projesi.Entities;
using System.ComponentModel.DataAnnotations;


namespace Bitirme_Projesi.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        public string PasswordRepeat { get; set; }
        public string Role { get; set; } = "user";

    }
}
