using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Repositories
{
    public interface IEmergencyContactRepository
    {
        Task<PagedResponse<IEnumerable<EmergencyContact>>> ListAsync(PaginationParameter filter);
        Task<EmergencyContact?> FindByIdAsync(Guid id);
        Task SaveAsync(EmergencyContact emergencyContact);
        void Update(EmergencyContact emergencyContact);
        void Remove(EmergencyContact emergencyContact);
    }
}
