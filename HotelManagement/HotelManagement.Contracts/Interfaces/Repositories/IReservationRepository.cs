using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<PagedResponse<IEnumerable<Reservation>>> ListAsync(PaginationParameter filter);
        Task<Reservation?> FindByIdAsync(Guid id);
        Task SaveAsync(Reservation reservation);
        void Update(Reservation reservation);
        void Remove(Reservation reservation);
    }
}
