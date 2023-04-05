using HotelManagement.Contracts.Helpers;
using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using HotelManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository.Repositories
{
    public class HotelRepository : BaseRepository, IHotelRepository
    {
        public HotelRepository(AppDbContext context) : base(context) { }

        public async Task<PagedResponse<IEnumerable<Hotel>>> ListAsync(PaginationParameter filter)
        {
            return await _context.Hotels
                                 .AsNoTracking()
                                 .Include(r => r.Rooms).ThenInclude(t => t.RoomType)
                                 .Include(r => r.Rooms).ThenInclude(l => l.Location)
                                 .PaginateAsync(filter);
        }

        public async Task<Hotel?> FindByIdAsync(Guid id)
        {
            return await _context.Hotels
                                 .AsNoTracking()
                                 .Include(r => r.Rooms).ThenInclude(t => t.RoomType)
                                 .Include(r => r.Rooms).ThenInclude(l => l.Location)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            _context.SaveChanges();
        }

        public void Update(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            _context.SaveChanges();
        }

        public void Remove(Hotel hotel)
        {
            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
        }
    }
}
