using HotelManagement.Contracts.Helpers;
using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using HotelManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository.Repositories
{
    public class AgencyRepository : BaseRepository, IAgencyRepository
    {
        public AgencyRepository(AppDbContext context) : base(context) { }

        public async Task<PagedResponse<IEnumerable<Agency>>> ListAsync(PaginationParameter filter)
        {
            return await _context.Agencies
                                 .AsNoTracking()
                                 .Include(h => h.Hotels).ThenInclude(r => r.Rooms).ThenInclude(t => t.RoomType)
                                 .Include(h => h.Hotels).ThenInclude(r => r.Rooms).ThenInclude(t => t.Location)
                                 .PaginateAsync(filter);
        }

        public async Task<Agency?> FindByIdAsync(Guid id)
        {
            return await _context.Agencies
                                 .AsNoTracking()
                                 .Include(h => h.Hotels).ThenInclude(r => r.Rooms).ThenInclude(t => t.RoomType)
                                 .Include(h => h.Hotels).ThenInclude(r => r.Rooms).ThenInclude(t => t.Location)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(Agency agency)
        {
            await _context.Agencies.AddAsync(agency);
            _context.SaveChanges();
        }

        public void Update(Agency agency)
        {
            _context.Agencies.Update(agency);
            _context.SaveChanges();
        }

        public void Remove(Agency agency)
        {
            _context.Agencies.Remove(agency);
            _context.SaveChanges();
        }
    }
}
