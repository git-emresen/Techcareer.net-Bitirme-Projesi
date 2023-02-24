
using Microsoft.EntityFrameworkCore;

namespace Bitirme_Projesi.Entities
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
	}
}
