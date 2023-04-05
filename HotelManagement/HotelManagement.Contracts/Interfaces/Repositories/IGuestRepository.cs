using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Repositories
{
    public interface IGuestRepository
    {
        Task<PagedResponse<IEnumerable<Guest>>> ListAsync(PaginationParameter filter);
        Task<Guest?> FindByIdAsync(Guid id);
        Task SaveAsync(Guest guest);
        void Update(Guest guest);
        void Remove(Guest guest);
    }
}
