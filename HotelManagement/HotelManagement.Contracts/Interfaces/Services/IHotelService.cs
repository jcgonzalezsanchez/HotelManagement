using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Services
{
    public interface IHotelService
    {
        Task<PagedResponse<IEnumerable<Hotel>>> ListAsync(PaginationParameter filter);
        Task<HotelResponse> FindByIdAsync(Guid id);
        Task<HotelResponse> SaveAsync(Hotel hotel);
        Task<HotelResponse> Update(Hotel hotel);
        Task<HotelResponse> Delete(Guid id);
    }
}
