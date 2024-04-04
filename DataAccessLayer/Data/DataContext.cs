using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("BarberBook_Connection");
                optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly("BarberLayered"));
            }
        }

        public DbSet<BarberShop> BarberShops { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<RegistrationKey> RegistrationKeys { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
