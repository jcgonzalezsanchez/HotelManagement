using HotelManagement.Contracts.Models;

namespace HotelManagement.Contracts.Response
{
    public class RoomTypeResponse : BaseResponse<RoomType>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="roomType">Saved RoomType.</param>
        /// <returns>Response.</returns>
        public RoomTypeResponse(RoomType roomType) : base(roomType)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RoomTypeResponse(string message) : base(message)
        { }
    }
}
