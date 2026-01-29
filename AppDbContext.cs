using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions options) : base(options)
	{
	}

	DbSet<Location> Locations => Set<Location>();
}
