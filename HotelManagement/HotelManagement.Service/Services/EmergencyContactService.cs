using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Interfaces.Services;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Service.Services
{
    public class EmergencyContactService : IEmergencyContactService
    {
        private readonly IEmergencyContactRepository _emergencyContactRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<EmergencyContactService> _logger;

        public EmergencyContactService(IEmergencyContactRepository emergencyContactRepository, IReservationRepository reservationRepository, ILogger<EmergencyContactService> logger)
        {
            _emergencyContactRepository = emergencyContactRepository;
            _reservationRepository = reservationRepository;
            _logger = logger;

        }
        public async Task<PagedResponse<IEnumerable<EmergencyContact>>> ListAsync(PaginationParameter filter)
        {
            var emergencyContacts = await _emergencyContactRepository.ListAsync(filter);

            if (emergencyContacts.Resource.Any())
                _logger.LogInformation($"EmergencyContact Service - {emergencyContacts.Resource.Count()} Emergency Contacts listed successfully.");
            else
                _logger.LogError("EmergencyContact Service - No Emergency Contacts to list.");

            return emergencyContacts;
        }

        public async Task<EmergencyContactResponse> FindByIdAsync(Guid id)
        {
            try
            {
                var emergencyContact = await _emergencyContactRepository.FindByIdAsync(id);

                if (emergencyContact == null)
                {
                    _logger.LogInformation($"EmergencyContact Service - EmergencyContact Id '{id}' not found.");
                    return new EmergencyContactResponse("EmergencyContact not found.");
                }

                return new EmergencyContactResponse(emergencyContact);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmergencyContact Service - Error retrieving the EmergencyContact: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmergencyContactResponse($"An error occurred when retrieving the EmergencyContact: {ex.Message}");
            }
        }

        public async Task<EmergencyContactResponse> SaveAsync(EmergencyContact emergencyContact)
        {
            try
            {
                var existingReservation = await _reservationRepository.FindByIdAsync(emergencyContact.ReservationId);

                if (existingReservation == null)
                {
                    _logger.LogInformation($"EmergencyContact Service - Reservation Id '{0}' not found.", emergencyContact.ReservationId);
                    return new EmergencyContactResponse("Reservation Id not found.");
                }

                emergencyContact.Id = Guid.NewGuid();
                await _emergencyContactRepository.SaveAsync(emergencyContact);
                _logger.LogInformation($"EmergencyContact Service - EmergencyContact Id '{emergencyContact.Id}' saved successfully.");

                return new EmergencyContactResponse(emergencyContact);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmergencyContact Service - Error saving the EmergencyContact: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmergencyContactResponse($"An error occurred when saving the EmergencyContact: {ex.Message}");
            }
        }

        public async Task<EmergencyContactResponse> Update(EmergencyContact emergencyContact)
        {
            var existingEmergencyContact = await _emergencyContactRepository.FindByIdAsync(emergencyContact.Id);

            if (existingEmergencyContact == null)
            {
                _logger.LogInformation($"EmergencyContact Service - EmergencyContact Id '{0}' not found.", emergencyContact.Id);
                return new EmergencyContactResponse("EmergencyContact not found.");
            }

            try
            {
                _emergencyContactRepository.Update(emergencyContact);
                _logger.LogInformation($"EmergencyContact Service - EmergencyContact Id '{0}' updated successfully.", emergencyContact.Id);
                return new EmergencyContactResponse(emergencyContact);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmergencyContact Service - Error updating the EmergencyContact: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmergencyContactResponse($"An error occurred when updating the EmergencyContact: {ex.Message}");
            }
        }

        public async Task<EmergencyContactResponse> Delete(Guid id)
        {
            var existingEmergencyContact = await _emergencyContactRepository.FindByIdAsync(id);

            if (existingEmergencyContact == null)
            {
                _logger.LogInformation($"EmergencyContact Service - EmergencyContact Id '{0}' not found.", id);
                return new EmergencyContactResponse("EmergencyContact not found.");
            }

            try
            {
                _emergencyContactRepository.Remove(existingEmergencyContact);
                _logger.LogInformation($"EmergencyContact Service - EmergencyContact Id '{0}' deleted successfully.", id);
                return new EmergencyContactResponse(existingEmergencyContact);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmergencyContact Service - Error deleting the EmergencyContact: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmergencyContactResponse($"An error occurred when deleting the EmergencyContact: {ex.Message}");
            }
        }
    }
}
