using BarberLayered.Filters;
using BuinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Log Filter
builder.Services.AddControllers(config => config.Filters.Add<LogActionFilter>());
builder.Services.AddScoped<LogActionFilter>();

// Repositories
builder.Services.AddScoped<IBarberRepository, BarberRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();


// Services
builder.Services.AddScoped<IBarberService, BarberService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();


// DbContext
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
    pattern: "{controller=BarberShop}/{action=Index}/{id?}");

app.Run();
