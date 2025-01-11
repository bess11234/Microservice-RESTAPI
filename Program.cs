using Microsoft.EntityFrameworkCore;
using LawsuitM.Models;

using System.Text.Json; // Package JSON

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LawsuitContext>(opt =>
    opt.UseInMemoryDatabase("Lawsuit"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = builder.Environment.ApplicationName,
        Version = "v1"
    });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // UseSwaggerUI Protected by if (env.IsDevelopment())
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                    $"{builder.Environment.ApplicationName} v1"));
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LawsuitContext>();
    SeedDatabaseFromJson(context);
}

app.MapControllers();
app.UseRouting();
app.Run();


// Method to seed the database from JSON
void SeedDatabaseFromJson(LawsuitContext context)
{
    if (!context.LawsuitItem.Any())
    {
        var jsonFilePath = "seed.json"; // Path to your JSON file
        var jsonData = File.ReadAllText(jsonFilePath); // Read the file
        var lawsuits = JsonSerializer.Deserialize<List<LawsuitItem>>(jsonData); // Deserialize JSON

        if (lawsuits != null)
        {
            context.LawsuitItem.AddRange(lawsuits); // Add data to the DbSet
            context.SaveChanges(); // Save changes to the database
        }
    }
}