using HotelManagement.Contracts.Helpers;
using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using HotelManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository.Repositories
{
    public class EmergencyContactRepository : BaseRepository, IEmergencyContactRepository
    {
        public EmergencyContactRepository(AppDbContext context) : base(context) { }

        public async Task<PagedResponse<IEnumerable<EmergencyContact>>> ListAsync(PaginationParameter filter)
        {
            return await _context.EmergencyContacts
                                 .AsNoTracking()
                                 .PaginateAsync(filter);
        }

        public async Task<EmergencyContact?> FindByIdAsync(Guid id)
        {
            return await _context.EmergencyContacts
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(EmergencyContact emergencyContact)
        {
            await _context.EmergencyContacts.AddAsync(emergencyContact);
            _context.SaveChanges();
        }

        public void Update(EmergencyContact emergencyContact)
        {
            _context.EmergencyContacts.Update(emergencyContact);
            _context.SaveChanges();
        }

        public void Remove(EmergencyContact emergencyContact)
        {
            _context.EmergencyContacts.Remove(emergencyContact);
            _context.SaveChanges();
        }
    }
}
