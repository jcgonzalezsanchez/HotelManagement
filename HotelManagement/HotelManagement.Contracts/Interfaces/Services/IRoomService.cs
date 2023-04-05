using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Services
{
    public interface IRoomService
    {
        Task<PagedResponse<IEnumerable<Room>>> ListAsync(PaginationParameter filter);
        Task<RoomResponse> FindByIdAsync(Guid id);
        Task<RoomResponse> SaveAsync(Room room);
        Task<RoomResponse> Update(Room room);
        Task<RoomResponse> Delete(Guid id);
    }
}
