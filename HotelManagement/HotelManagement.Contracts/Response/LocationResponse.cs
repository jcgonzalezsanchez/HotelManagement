using HotelManagement.Contracts.Models;

namespace HotelManagement.Contracts.Response
{
    public class LocationResponse : BaseResponse<Location>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="location">Saved Location.</param>
        /// <returns>Response.</returns>
        public LocationResponse(Location location) : base(location)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public LocationResponse(string message) : base(message)
        { }
    }
}
