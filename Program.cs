using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using price_management_api.Data;
using price_management_api.Interfaces;
using price_management_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register Service Dependency Injection Container
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IItemPriceService, ItemPriceService>();

// Allow Frontend (Next.js) to access API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure OpenAPI / Scalar 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title         = "Price Management API";
        options.Theme         = ScalarTheme.DeepSpace;
        options.DefaultFonts  = false;
        options.HideClientButton = true;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();

var baseUrl = app.Configuration["ASPNETCORE_URLS"];
var firstUrl = baseUrl.Split(';')[0].TrimEnd('/');

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine($"\n  App:     {firstUrl}/");
Console.WriteLine($"  Scalar:  {firstUrl}/scalar/v1");
Console.WriteLine();
Console.ResetColor();

app.Run();
