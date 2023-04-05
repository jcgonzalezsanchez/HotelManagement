using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Services
{
    public interface IReservationService
    {
        Task<PagedResponse<IEnumerable<Reservation>>> ListAsync(PaginationParameter filter);
        Task<ReservationResponse> FindByIdAsync(Guid id);
        Task<ReservationResponse> SaveAsync(Reservation reservation);
        Task<ReservationResponse> Update(Reservation reservation);
        Task<ReservationResponse> Delete(Guid id);
    }
}
