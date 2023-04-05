using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Interfaces.Services;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Service.Services
{
    public class AgencyService : IAgencyService
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly ILogger<AgencyService> _logger;

        public AgencyService(IAgencyRepository agencyRepository, ILogger<AgencyService> logger)
        {
            _agencyRepository = agencyRepository;
            _logger = logger;

        }
        public async Task<PagedResponse<IEnumerable<Agency>>> ListAsync(PaginationParameter filter)
        {
            var agencies = await _agencyRepository.ListAsync(filter);

            if (agencies.Resource.Any())
                _logger.LogInformation($"Agency Service - {agencies.Resource.Count()} Agencies listed successfully.");
            else
                _logger.LogError("Agency Service - No Agencies to list.");

            return agencies;
        }

        public async Task<AgencyResponse> FindByIdAsync(Guid id)
        {
            try
            {
                var agency = await _agencyRepository.FindByIdAsync(id);

                if (agency == null)
                {
                    _logger.LogInformation($"Agency Service - Agency Id '{id}' not found.");
                    return new AgencyResponse("Agency not found.");
                }

                return new AgencyResponse(agency);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Agency Service - Error retrieving the Agency: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new AgencyResponse($"An error occurred when retrieving the Agency: {ex.Message}");
            }
        }

        public async Task<AgencyResponse> SaveAsync(Agency agency)
        {
            try
            {
                agency.Id = Guid.NewGuid();
                await _agencyRepository.SaveAsync(agency);
                _logger.LogInformation($"Agency Service - Agency Id '{agency.Id}' saved successfully.");

                return new AgencyResponse(agency);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Agency Service - Error saving the Agency: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new AgencyResponse($"An error occurred when saving the Agency: {ex.Message}");
            }
        }

        public async Task<AgencyResponse> Update(Agency agency)
        {
            var existingAgency = await _agencyRepository.FindByIdAsync(agency.Id);

            if (existingAgency == null)
            {
                _logger.LogInformation($"Agency Service - Agency Id '{0}' not found.", agency.Id);
                return new AgencyResponse("Agency not found.");
            }

            try
            {
                _agencyRepository.Update(agency);
                _logger.LogInformation($"Agency Service - Agency Id '{0}' updated successfully.", agency.Id);
                return new AgencyResponse(agency);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Agency Service - Error updating the Agency: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new AgencyResponse($"An error occurred when updating the Agency: {ex.Message}");
            }
        }

        public async Task<AgencyResponse> Delete(Guid id)
        {
            var existingAgency = await _agencyRepository.FindByIdAsync(id);

            if (existingAgency == null)
            {
                _logger.LogInformation($"Agency Service - Agency Id '{0}' not found.", id);
                return new AgencyResponse("Agency not found.");
            }

            try
            {
                _agencyRepository.Remove(existingAgency);
                _logger.LogInformation($"Agency Service - Agency Id '{0}' deleted successfully.", id);
                return new AgencyResponse(existingAgency);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Agency Service - Error deleting the Agency: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new AgencyResponse($"An error occurred when deleting the Agency: {ex.Message}");
            }
        }
    }
}
