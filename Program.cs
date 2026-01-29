using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5171");

// Lägg till den DbContext ni skapar för er databas här
// builder.Services.AddDbContext<ApiDbContext>(options =>
//    options.UseInMemoryDatabase("EventDb"));

//Låt detta vara kvar! Utan denna inställning kommer inte websidan att få access till API:et.
// Läs mer här: https://developer.mozilla.org/en-US/docs/Web/HTTP/Guides/CORS
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data source=database.db"));

var app = builder.Build();

using var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
// db.Database.EnsureDeleted();
db.Database.EnsureCreated();

SeedDatabase(db);

// Denna hör ihop med CORS-inställningen ovan
app.UseCors();

//Ni ska inte skriva era endpoints här i Program.cs utan i separata controllers, så använd denna:
app.MapControllers();

app.Run();

void SeedDatabase(AppDbContext context)
{
	if (context.Locations.Any()) return;

	var csvPath = "locations.csv";

	var locations = File.ReadAllLines(csvPath)
		.Select(line =>
		{
			var parts = line.Split(',');
			return new Location
			{
				City = parts[0].Trim(),
				Latitude = parts[1].Trim(),
				Longitude = parts[2].Trim(),
				Country = parts[3].Trim(),
				Region = parts[4].Trim()
			};
		})
		.ToList();

	context.Locations.AddRange(locations);
	context.SaveChanges();
}