using HotelManagement.Contracts.Models;

namespace HotelManagement.Contracts.Response
{
    public class GuestResponse : BaseResponse<Guest>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="guest">Saved Guest.</param>
        /// <returns>Response.</returns>
        public GuestResponse(Guest guest) : base(guest)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public GuestResponse(string message) : base(message)
        { }
    }
}
