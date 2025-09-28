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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.Run();