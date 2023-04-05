using HotelManagement.Contracts.Models;

namespace HotelManagement.Contracts.Response
{
    public class ReservationResponse : BaseResponse<Reservation>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="reservation">Saved Reservation.</param>
        /// <returns>Response.</returns>
        public ReservationResponse(Reservation reservation) : base(reservation)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ReservationResponse(string message) : base(message)
        { }
    }
}
