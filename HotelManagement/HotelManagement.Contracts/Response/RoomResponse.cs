using HotelManagement.Contracts.Models;

namespace HotelManagement.Contracts.Response
{
    public class RoomResponse : BaseResponse<Room>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="room">Saved Room.</param>
        /// <returns>Response.</returns>
        public RoomResponse(Room room) : base(room)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RoomResponse(string message) : base(message)
        { }
    }
}
