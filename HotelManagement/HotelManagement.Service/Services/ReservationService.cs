using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Interfaces.Services;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Service.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<ReservationService> _logger;

        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository, ILogger<ReservationService> logger)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _logger = logger;

        }
        public async Task<PagedResponse<IEnumerable<Reservation>>> ListAsync(PaginationParameter filter)
        {
            var reservations = await _reservationRepository.ListAsync(filter);

            if (reservations.Resource.Any())
                _logger.LogInformation($"Reservation Service - {reservations.Resource.Count()} Reservations listed successfully.");
            else
                _logger.LogError("Reservation Service - No Reservations to list.");

            return reservations;
        }

        public async Task<ReservationResponse> FindByIdAsync(Guid id)
        {
            try
            {
                var reservation = await _reservationRepository.FindByIdAsync(id);

                if (reservation == null)
                {
                    _logger.LogInformation($"Reservation Service - Reservation Id '{id}' not found.");
                    return new ReservationResponse("Reservation not found.");
                }

                return new ReservationResponse(reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservation Service - Error retrieving the Reservation: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new ReservationResponse($"An error occurred when retrieving the Reservation: {ex.Message}");
            }
        }

        public async Task<ReservationResponse> SaveAsync(Reservation reservation)
        {
            try
            {
                var existingRoom = await _roomRepository.FindByIdAsync(reservation.RoomId);

                if (existingRoom == null)
                {
                    _logger.LogInformation($"Reservation Service - Room Id '{0}' not found.", reservation.RoomId);
                    return new ReservationResponse("Room Id not found.");
                }

                reservation.Id = Guid.NewGuid();
                reservation.Guests?.ToList().ForEach(x => x.Id = Guid.NewGuid());
                reservation.Guests?.ToList().ForEach(x => x.ReservationId = reservation.Id);
                reservation.EmergencyContacts?.ToList().ForEach(x => x.Id = Guid.NewGuid());
                reservation.EmergencyContacts?.ToList().ForEach(x => x.ReservationId = reservation.Id);

                await _reservationRepository.SaveAsync(reservation);
                _logger.LogInformation($"Reservation Service - Reservation Id '{reservation.Id}' saved successfully.");

                return new ReservationResponse(reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservation Service - Error saving the Reservation: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new ReservationResponse($"An error occurred when saving the Reservation: {ex.Message}");
            }
        }

        public async Task<ReservationResponse> Update(Reservation reservation)
        {
            var existingReservation = await _reservationRepository.FindByIdAsync(reservation.Id);

            if (existingReservation == null)
            {
                _logger.LogInformation($"Reservation Service - Reservation Id '{0}' not found.", reservation.Id);
                return new ReservationResponse("Reservation not found.");
            }

            try
            {
                _reservationRepository.Update(reservation);
                _logger.LogInformation($"Reservation Service - Reservation Id '{0}' updated successfully.", reservation.Id);
                return new ReservationResponse(reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservation Service - Error updating the Reservation: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new ReservationResponse($"An error occurred when updating the Reservation: {ex.Message}");
            }
        }

        public async Task<ReservationResponse> Delete(Guid id)
        {
            var existingReservation = await _reservationRepository.FindByIdAsync(id);

            if (existingReservation == null)
            {
                _logger.LogInformation($"Reservation Service - Reservation Id '{0}' not found.", id);
                return new ReservationResponse("Reservation not found.");
            }

            try
            {
                _reservationRepository.Remove(existingReservation);
                _logger.LogInformation($"Reservation Service - Reservation Id '{0}' deleted successfully.", id);
                return new ReservationResponse(existingReservation);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservation Service - Error deleting the Reservation: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new ReservationResponse($"An error occurred when deleting the Reservation: {ex.Message}");
            }
        }
    }
}
