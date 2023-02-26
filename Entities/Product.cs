using Bitirme_Projesi.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bitirme_Projesi.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }       
        public string? ProductDescription { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string? Image { get; set; }


    }
}
