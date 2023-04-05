using HotelManagement.Contracts.Helpers;
using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using HotelManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository.Repositories
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context) { }

        public async Task<PagedResponse<IEnumerable<Room>>> ListAsync(PaginationParameter filter)
        {
            return await _context.Rooms
                                 .AsNoTracking()
                                 .Include(t => t.RoomType)
                                 .Include(t => t.Location)
                                 .PaginateAsync(filter);
        }

        public async Task<Room?> FindByIdAsync(Guid id)
        {
            return await _context.Rooms
                                 .AsNoTracking()
                                 .Include(t => t.RoomType)
                                 .Include(t => t.Location)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            _context.SaveChanges();
        }

        public void Update(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
        }

        public void Remove(Room room)
        {
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}
