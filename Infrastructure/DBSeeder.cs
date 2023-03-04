using Bitirme_Projesi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bitirme_Projesi.Infrastructure
{
    public class DBSeeder
    {
        private static ApplicationDbContext _context;
       
        public static void Seed(ApplicationDbContext context)
        {
            _context = context;
            // context.Database.EnsureCreated() does not use migrations to create the database and therefore the database that is created cannot be later updated using migrations 
            // use context.Database.Migrate() instead
            _context.Database.Migrate();

            if (_context.Users.Any())
            {
                return;
            }

            // insert dummy data
            _context.AddRange(GetUser());
            _context.AddRange(GetCategory());   
            _context.AddRange(GetProduct());    
           
            _context.SaveChanges();
        }
        public static List<User> GetUser()
        {
            var user = new List<User>
        {
            new User { FirstName="admin",LastName="admin",Email="admin@admin.com",Password="Admin123123",PasswordRepeat="Admin123123",Role="admin"},
            new User { FirstName="user",LastName="user",Email="user@user.com",Password="User123123",PasswordRepeat="User123123",Role="user"},
        };
            return user;
        }
        public static List<Category> GetCategory()
        {
            var category = new List<Category>
            {
                new Category{CategoryName="bilgisayar"},
                new Category{CategoryName="telefon"},
                new Category{CategoryName="tablet"},
                new Category{CategoryName="beyaz eşya"},
                new Category{CategoryName="ev eşyası"}
            };
            return category;
        }

        public static List<Product> GetProduct()
        {
            var product = new List<Product>
            {
                new Product{ProductName="laptop",ProductDescription="yeni model laptop",UnitPrice=11200,UnitsInStock=200,Image="Images/laptop.png"},
                new Product{ProductName="cep telefonu",ProductDescription="son model cep telefonu",UnitPrice=9000,UnitsInStock=200,Image="Images/telefon.jpg"},
                new Product{ProductName="tablet",ProductDescription="yüksek çözünürlüklü tablet",UnitPrice=1200,UnitsInStock=200,Image="Images/tablet.png"},
                new Product{ProductName="mikrodalga",ProductDescription="kaliteli mikrodalga",UnitPrice=2200,UnitsInStock=200,Image="Images/mikrodalya.png"},
                new Product{ProductName="süpürge",ProductDescription="hızlı ev temizleyici",UnitPrice=6500,UnitsInStock=200,Image="Images/süpürge.jpg"},

            };
            return product;
        }

    }
   
     
}
