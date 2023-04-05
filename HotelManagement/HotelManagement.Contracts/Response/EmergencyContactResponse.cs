using HotelManagement.Contracts.Models;

namespace HotelManagement.Contracts.Response
{
    public class EmergencyContactResponse : BaseResponse<EmergencyContact>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="emergencyContact">Saved EmergencyContact.</param>
        /// <returns>Response.</returns>
        public EmergencyContactResponse(EmergencyContact emergencyContact) : base(emergencyContact)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public EmergencyContactResponse(string message) : base(message)
        { }
    }
}
