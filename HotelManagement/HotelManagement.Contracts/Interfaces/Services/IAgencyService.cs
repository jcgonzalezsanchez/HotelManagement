using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;

namespace HotelManagement.Contracts.Interfaces.Services
{
    public interface IAgencyService
    {
        Task<PagedResponse<IEnumerable<Agency>>> ListAsync(PaginationParameter filter);
        Task<AgencyResponse> FindByIdAsync(Guid id);
        Task<AgencyResponse> SaveAsync(Agency agency);
        Task<AgencyResponse> Update(Agency agency);
        Task<AgencyResponse> Delete(Guid id);
    }
}
