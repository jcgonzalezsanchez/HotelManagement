using HotelManagement.Contracts.Models;

namespace HotelManagement.Contracts.Response
{
    public class HotelResponse : BaseResponse<Hotel>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="hotel">Saved Hotel.</param>
        /// <returns>Response.</returns>
        public HotelResponse(Hotel hotel) : base(hotel)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public HotelResponse(string message) : base(message)
        { }
    }
}
