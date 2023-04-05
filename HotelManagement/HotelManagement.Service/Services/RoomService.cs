using HotelManagement.Contracts.Interfaces.Repositories;
using HotelManagement.Contracts.Interfaces.Services;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly ILogger<RoomService> _logger;

        public RoomService(IRoomRepository roomRepository, IHotelRepository hotelRepository, ILogger<RoomService> logger)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _logger = logger;

        }
        public async Task<PagedResponse<IEnumerable<Room>>> ListAsync(PaginationParameter filter)
        {
            var rooms = await _roomRepository.ListAsync(filter);

            if (rooms.Resource.Any())
                _logger.LogInformation($"Room Service - {rooms.Resource.Count()} Rooms listed successfully.");
            else
                _logger.LogError("Room Service - No Rooms to list.");

            return rooms;
        }

        public async Task<RoomResponse> FindByIdAsync(Guid id)
        {
            try
            {
                var room = await _roomRepository.FindByIdAsync(id);

                if (room == null)
                {
                    _logger.LogInformation($"Room Service - Room Id '{id}' not found.");
                    return new RoomResponse("Room not found.");
                }

                return new RoomResponse(room);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Room Service - Error retrieving the Room: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new RoomResponse($"An error occurred when retrieving the Room: {ex.Message}");
            }
        }

        public async Task<RoomResponse> SaveAsync(Room room)
        {
            try
            {
                var existingHotel = await _hotelRepository.FindByIdAsync(room.HotelId);

                if (existingHotel == null)
                {
                    _logger.LogInformation($"Room Service - Hotel Id '{0}' not found.", room.HotelId);
                    return new RoomResponse("Hotel Id not found.");
                }

                room.Id = Guid.NewGuid();
                room.RoomType.Id = Guid.NewGuid();
                room.Location.Id = Guid.NewGuid();

                await _roomRepository.SaveAsync(room);
                _logger.LogInformation($"Room Service - Room Id '{room.Id}' saved successfully.");

                return new RoomResponse(room);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Room Service - Error saving the Room: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new RoomResponse($"An error occurred when saving the Room: {ex.Message}");
            }
        }

        public async Task<RoomResponse> Update(Room room)
        {
            var existingRoom = await _roomRepository.FindByIdAsync(room.Id);

            if (existingRoom == null)
            {
                _logger.LogInformation($"Room Service - Room Id '{0}' not found.", room.Id);
                return new RoomResponse("Room not found.");
            }

            try
            {
                _roomRepository.Update(room);
                _logger.LogInformation($"Room Service - Room Id '{0}' updated successfully.", room.Id);
                return new RoomResponse(room);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Room Service - Error updating the Room: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new RoomResponse($"An error occurred when updating the Room: {ex.Message}");
            }
        }

        public async Task<RoomResponse> Delete(Guid id)
        {
            var existingRoom = await _roomRepository.FindByIdAsync(id);

            if (existingRoom == null)
            {
                _logger.LogInformation($"Room Service - Room Id '{0}' not found.", id);
                return new RoomResponse("Room not found.");
            }

            try
            {
                _roomRepository.Remove(existingRoom);
                _logger.LogInformation($"Room Service - Room Id '{0}' deleted successfully.", id);
                return new RoomResponse(existingRoom);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Room Service - Error deleting the Room: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new RoomResponse($"An error occurred when deleting the Room: {ex.Message}");
            }
        }
    }
}
