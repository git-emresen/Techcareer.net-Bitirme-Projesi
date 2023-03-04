
using Microsoft.EntityFrameworkCore;

namespace Bitirme_Projesi.Entities
{
    public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
		public DbSet<UserList> UserLists { get; set; }
	}
}
