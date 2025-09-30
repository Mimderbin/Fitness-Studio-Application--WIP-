using FitAPI.Data;
using FitAPI.Models;
using FitAPI.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(typeof(IValidationService<>), typeof(ValidationService<>));
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging());

builder.Services.AddControllers(options =>
    options.Filters.Add(new EnableQueryAttribute
    {
        MaxTop = 100,
        MaxExpansionDepth = 5
    }))
    .AddOData(opt => opt
        .Select()
        .Filter()
        .Expand()
        .Count()
        .OrderBy()
        .AddRouteComponents("odata", GetEdmModel()));

IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Kunde>("Kunden");
    builder.EntitySet<Admin>("Admins");
    return builder.GetEdmModel();
}

var app = builder.Build();
app.UseRouting();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<AppDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        db.Database.Migrate();
        logger.LogInformation("Database migration applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying database migrations.");
        throw;
    }
}

app.UseDeveloperExceptionPage();

app.Run();