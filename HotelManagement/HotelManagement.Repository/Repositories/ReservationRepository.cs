using HotelManagement.Contracts.Helpers;
using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using HotelManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository.Repositories
{
    public class ReservationRepository : BaseRepository, IReservationRepository
    {
        public ReservationRepository(AppDbContext context) : base(context) { }

        public async Task<PagedResponse<IEnumerable<Reservation>>> ListAsync(PaginationParameter filter)
        {
            return await _context.Reservations
                                 .AsNoTracking()
                                 .Include(g => g.Guests)
                                 .Include(g => g.EmergencyContacts)
                                 .PaginateAsync(filter);
        }

        public async Task<Reservation?> FindByIdAsync(Guid id)
        {
            return await _context.Reservations
                                 .AsNoTracking()
                                 .Include(g => g.Guests)
                                 .Include(g => g.EmergencyContacts)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            _context.SaveChanges();
        }

        public void Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }

        public void Remove(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }
    }
}
