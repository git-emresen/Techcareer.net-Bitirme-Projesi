using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bitirme_Projesi.Entities
{
    public class UserList
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool? IsDone { get;set; } = false;   
        public virtual int UserId { get; set; }    
    }
}
