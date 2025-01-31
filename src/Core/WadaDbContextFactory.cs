using Core;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

public class WadaDbContextFactory : IDesignTimeDbContextFactory<WadaDbContext>
{
	public WadaDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<WadaDbContext>();
		optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=wada;User Id=username;Password=password;");

		return new WadaDbContext(optionsBuilder.Options);
	}
}
