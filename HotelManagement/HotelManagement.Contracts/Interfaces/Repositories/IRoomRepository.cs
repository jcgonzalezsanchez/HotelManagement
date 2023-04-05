using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<PagedResponse<IEnumerable<Room>>> ListAsync(PaginationParameter filter);
        Task<Room?> FindByIdAsync(Guid id);
        Task SaveAsync(Room room);
        void Update(Room room);
        void Remove(Room room);
    }
}
