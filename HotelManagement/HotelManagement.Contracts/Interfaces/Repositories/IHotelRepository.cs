using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Repositories
{
    public interface IHotelRepository
    {
        Task<PagedResponse<IEnumerable<Hotel>>> ListAsync(PaginationParameter filter);
        Task<Hotel?> FindByIdAsync(Guid id);
        Task SaveAsync(Hotel hotel);
        void Update(Hotel hotel);
        void Remove(Hotel hotel);
    }
}
