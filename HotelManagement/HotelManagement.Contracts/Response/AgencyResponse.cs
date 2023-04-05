using HotelManagement.Contracts.Models;

namespace HotelManagement.Contracts.Response
{
    public class AgencyResponse : BaseResponse<Agency>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="agency">Saved Agency.</param>
        /// <returns>Response.</returns>
        public AgencyResponse(Agency agency) : base(agency)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public AgencyResponse(string message) : base(message)
        { }
    }
}
