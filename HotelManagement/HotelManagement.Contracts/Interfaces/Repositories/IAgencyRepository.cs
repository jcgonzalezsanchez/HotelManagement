using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Repositories
{
    public interface IAgencyRepository
    {
        Task<PagedResponse<IEnumerable<Agency>>> ListAsync(PaginationParameter filter);
        Task<Agency?> FindByIdAsync(Guid id);
        Task SaveAsync(Agency agency);
        void Update(Agency agency);
        void Remove(Agency agency);
    }
}
