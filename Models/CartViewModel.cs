using Bitirme_Projesi.Entities;

namespace Bitirme_Projesi.Models
{
    
        public class CartViewModel
        {
            public List<CartItem> CartItems { get; set; }
            public decimal GrandTotal { get; set; }
        }
    
}
