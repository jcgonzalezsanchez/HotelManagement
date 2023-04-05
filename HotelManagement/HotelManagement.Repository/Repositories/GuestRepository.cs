using HotelManagement.Contracts.Helpers;
using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using HotelManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository.Repositories
{
    public class GuestRepository : BaseRepository, IGuestRepository
    {
        public GuestRepository(AppDbContext context) : base(context) { }

        public async Task<PagedResponse<IEnumerable<Guest>>> ListAsync(PaginationParameter filter)
        {
            return await _context.Guests
                                 .AsNoTracking()
                                 .PaginateAsync(filter);
        }

        public async Task<Guest?> FindByIdAsync(Guid id)
        {
            return await _context.Guests
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(Guest guest)
        {
            await _context.Guests.AddAsync(guest);
            _context.SaveChanges();
        }

        public void Update(Guest guest)
        {
            _context.Guests.Update(guest);
            _context.SaveChanges();
        }

        public void Remove(Guest guest)
        {
            _context.Guests.Remove(guest);
            _context.SaveChanges();
        }
    }
}
