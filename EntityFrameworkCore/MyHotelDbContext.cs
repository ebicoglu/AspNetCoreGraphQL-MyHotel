using Microsoft.EntityFrameworkCore;
using MyHotel.Entities;

namespace MyHotel.EntityFrameworkCore
{
    public class MyHotelDbContext : DbContext
    {
        public static string DbConnectionString = "Server=localhost; Database=MyHotelDb; Trusted_Connection=True;";

        public MyHotelDbContext(DbContextOptions<MyHotelDbContext> options)
            : base(options)
        { }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Room> Rooms { get; set; }
    }
}
