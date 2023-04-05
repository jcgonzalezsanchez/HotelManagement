using HotelManagement.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Guest> Guests { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
