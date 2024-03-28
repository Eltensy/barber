using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BarberLayered.Filters;
using Serilog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")  // Джерело конфігурації
    .Build();


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers(config => config.Filters.Add<LogActionFilter>());
builder.Services.AddScoped<LogActionFilter>();

builder.Services.AddDbContext<DataAccessLayer.Data.DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("BarberBook_Connection"), x => x.MigrationsAssembly("BarberLayered"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
