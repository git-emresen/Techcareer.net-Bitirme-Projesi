using Bitirme_Projesi.Models;
using Microsoft.EntityFrameworkCore;

namespace Bitirme_Projesi.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Account> Account { get; set; }
	}
}
