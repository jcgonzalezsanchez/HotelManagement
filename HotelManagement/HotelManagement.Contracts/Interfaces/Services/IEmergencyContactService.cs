using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Services
{
    public interface IEmergencyContactService
    {
        Task<PagedResponse<IEnumerable<EmergencyContact>>> ListAsync(PaginationParameter filter);
        Task<EmergencyContactResponse> FindByIdAsync(Guid id);
        Task<EmergencyContactResponse> SaveAsync(EmergencyContact emergencyContact);
        Task<EmergencyContactResponse> Update(EmergencyContact emergencyContact);
        Task<EmergencyContactResponse> Delete(Guid id);
    }
}
