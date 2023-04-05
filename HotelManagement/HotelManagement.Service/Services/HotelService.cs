using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Interfaces.Services;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Service.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly ILogger<HotelService> _logger;

        public HotelService(IHotelRepository hotelRepository, IAgencyRepository agencyRepository, ILogger<HotelService> logger)
        {
            _hotelRepository = hotelRepository;
            _agencyRepository = agencyRepository;
            _logger = logger;

        }
        public async Task<PagedResponse<IEnumerable<Hotel>>> ListAsync(PaginationParameter filter)
        {
            var hotels = await _hotelRepository.ListAsync(filter);

            if (hotels.Resource.Any())
                _logger.LogInformation($"Hotel Service - {hotels.Resource.Count()} Hotels listed successfully.");
            else
                _logger.LogError("Hotel Service - No Hotels to list.");

            return hotels;
        }

        public async Task<HotelResponse> FindByIdAsync(Guid id)
        {
            try
            {
                var hotel = await _hotelRepository.FindByIdAsync(id);

                if (hotel == null)
                {
                    _logger.LogInformation($"Hotel Service - Hotel Id '{id}' not found.");
                    return new HotelResponse("Hotel not found.");
                }

                return new HotelResponse(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hotel Service - Error retrieving the Hotel: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new HotelResponse($"An error occurred when retrieving the Hotel: {ex.Message}");
            }
        }

        public async Task<HotelResponse> SaveAsync(Hotel hotel)
        {
            try
            {
                var existingAgency = await _agencyRepository.FindByIdAsync(hotel.AgencyId);

                if (existingAgency == null)
                {
                    _logger.LogInformation($"Hotel Service - Agency Id '{0}' not found.", hotel.AgencyId);
                    return new HotelResponse("Agency Id not found.");
                }

                hotel.Id = Guid.NewGuid();
                hotel.Rooms?.ToList().ForEach(x => x.Id = Guid.NewGuid());
                hotel.Rooms?.ToList().ForEach(x => x.RoomType.Id = Guid.NewGuid());
                hotel.Rooms?.ToList().ForEach(x => x.RoomType.RoomId = x.Id);
                hotel.Rooms?.ToList().ForEach(x => x.Location.Id = Guid.NewGuid());
                hotel.Rooms?.ToList().ForEach(x => x.Location.RoomId = x.Id);
                hotel.Rooms?.ToList().ForEach(x => x.HotelId = hotel.Id);
                await _hotelRepository.SaveAsync(hotel);
                _logger.LogInformation($"Hotel Service - Hotel Id '{hotel.Id}' saved successfully.");

                return new HotelResponse(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hotel Service - Error saving the Hotel: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new HotelResponse($"An error occurred when saving the Hotel: {ex.Message}");
            }
        }

        public async Task<HotelResponse> Update(Hotel hotel)
        {
            var existingHotel = await _hotelRepository.FindByIdAsync(hotel.Id);

            if (existingHotel == null)
            {
                _logger.LogInformation($"Hotel Service - Hotel Id '{0}' not found.", hotel.Id);
                return new HotelResponse("Hotel not found.");
            }

            try
            {
                _hotelRepository.Update(hotel);
                _logger.LogInformation($"Hotel Service - Hotel Id '{0}' updated successfully.", hotel.Id);
                return new HotelResponse(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hotel Service - Error updating the Hotel: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new HotelResponse($"An error occurred when updating the Hotel: {ex.Message}");
            }
        }

        public async Task<HotelResponse> Delete(Guid id)
        {
            var existingHotel = await _hotelRepository.FindByIdAsync(id);

            if (existingHotel == null)
            {
                _logger.LogInformation($"Hotel Service - Hotel Id '{0}' not found.", id);
                return new HotelResponse("Hotel not found.");
            }

            try
            {
                _hotelRepository.Remove(existingHotel);
                _logger.LogInformation($"Hotel Service - Hotel Id '{0}' deleted successfully.", id);
                return new HotelResponse(existingHotel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hotel Service - Error deleting the Hotel: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new HotelResponse($"An error occurred when deleting the Hotel: {ex.Message}");
            }
        }
    }
}
