using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Interfaces.Services;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Service.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<GuestService> _logger;

        public GuestService(IGuestRepository guestRepository, IReservationRepository reservationRepository, ILogger<GuestService> logger)
        {
            _guestRepository = guestRepository;
            _reservationRepository = reservationRepository;
            _logger = logger;

        }
        public async Task<PagedResponse<IEnumerable<Guest>>> ListAsync(PaginationParameter filter)
        {
            var guests = await _guestRepository.ListAsync(filter);

            if (guests.Resource.Any())
                _logger.LogInformation($"Guest Service - {guests.Resource.Count()} Guests listed successfully.");
            else
                _logger.LogError("Guest Service - No Guests to list.");

            return guests;
        }

        public async Task<GuestResponse> FindByIdAsync(Guid id)
        {
            try
            {
                var guest = await _guestRepository.FindByIdAsync(id);

                if (guest == null)
                {
                    _logger.LogInformation($"Guest Service - Guest Id '{id}' not found.");
                    return new GuestResponse("Guest not found.");
                }

                return new GuestResponse(guest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Guest Service - Error retrieving the Guest: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new GuestResponse($"An error occurred when retrieving the Guest: {ex.Message}");
            }
        }

        public async Task<GuestResponse> SaveAsync(Guest guest)
        {
            try
            {
                var existingReservation = await _reservationRepository.FindByIdAsync(guest.ReservationId);

                if (existingReservation == null)
                {
                    _logger.LogInformation($"Guest Service - Reservation Id '{0}' not found.", guest.ReservationId);
                    return new GuestResponse("Reservation Id not found.");
                }

                guest.Id = Guid.NewGuid();
                await _guestRepository.SaveAsync(guest);
                _logger.LogInformation($"Guest Service - Guest Id '{guest.Id}' saved successfully.");

                return new GuestResponse(guest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Guest Service - Error saving the Guest: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new GuestResponse($"An error occurred when saving the Guest: {ex.Message}");
            }
        }

        public async Task<GuestResponse> Update(Guest guest)
        {
            var existingGuest = await _guestRepository.FindByIdAsync(guest.Id);

            if (existingGuest == null)
            {
                _logger.LogInformation($"Guest Service - Guest Id '{0}' not found.", guest.Id);
                return new GuestResponse("Guest not found.");
            }

            try
            {
                _guestRepository.Update(guest);
                _logger.LogInformation($"Guest Service - Guest Id '{0}' updated successfully.", guest.Id);
                return new GuestResponse(guest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Guest Service - Error updating the Guest: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new GuestResponse($"An error occurred when updating the Guest: {ex.Message}");
            }
        }

        public async Task<GuestResponse> Delete(Guid id)
        {
            var existingGuest = await _guestRepository.FindByIdAsync(id);

            if (existingGuest == null)
            {
                _logger.LogInformation($"Guest Service - Guest Id '{0}' not found.", id);
                return new GuestResponse("Guest not found.");
            }

            try
            {
                _guestRepository.Remove(existingGuest);
                _logger.LogInformation($"Guest Service - Guest Id '{0}' deleted successfully.", id);
                return new GuestResponse(existingGuest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Guest Service - Error deleting the Guest: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new GuestResponse($"An error occurred when deleting the Guest: {ex.Message}");
            }
        }
    }
}
