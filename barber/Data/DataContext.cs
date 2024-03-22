using barber.Entities;
using Microsoft.EntityFrameworkCore;

namespace barber.Data
{

    public class DataContext : DbContext
    {

        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
