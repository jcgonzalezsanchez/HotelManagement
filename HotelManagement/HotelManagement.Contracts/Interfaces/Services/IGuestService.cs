using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Services
{
    public interface IGuestService
    {
        Task<PagedResponse<IEnumerable<Guest>>> ListAsync(PaginationParameter filter);
        Task<GuestResponse> FindByIdAsync(Guid id);
        Task<GuestResponse> SaveAsync(Guest guest);
        Task<GuestResponse> Update(Guest guest);
        Task<GuestResponse> Delete(Guid id);
    }
}
