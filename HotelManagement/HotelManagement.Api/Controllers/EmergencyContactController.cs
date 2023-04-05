using HotelManagement.Contracts.Interfaces.Services;
using HotelManagement.Contracts.Models;
using HotelManagement.Contracts.Pagination;
using HotelManagement.Contracts.Resources;
using HotelManagement.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmergencyContactController : Controller
    {
        private readonly IEmergencyContactService _emergencyContact;

        public EmergencyContactController(IEmergencyContactService emergencyContact)
        {
            _emergencyContact = emergencyContact;
        }

        /// <summary>
        /// Get all Emergency Contacts.
        /// </summary>
        /// <param name="filter">The pagination filter</param>
        /// <returns>The list of Emergency Contacts</returns>
        /// <response code="200">The Emergency Contacts were successfully retrieved.</response> 
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<EmergencyContact>>), 200)]
        public async Task<PagedResponse<IEnumerable<EmergencyContact>>> ListAsync([FromQuery] PaginationParameter filter)
        {
            var validFilter = new PaginationParameter(filter.PageNumber, filter.PageSize);
            return await _emergencyContact.ListAsync(validFilter);
        }

        /// <summary>
        /// Get a Emergency Contact by id.
        /// </summary>
        /// <returns>The Emergency Contact.</returns>
        /// <response code="200">The Emergency ContactEmergency Contact was successfully retrieved.</response> 
        /// <response code="404">The Emergency Contact does not exist.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmergencyContact), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            var result = await _emergencyContact.FindByIdAsync(id);

            if (!result.Success)
            {
                return new ObjectResult(new ErrorResource(result.Message)) { StatusCode = 404 };
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Create a new Emergency Contact.
        /// </summary>
        /// <param name="emergencyContact">The Emergency Contact object.</param>
        /// <returns>The created Emergency Contact.</returns>
        /// <response code="201">The Emergency Contact was successfully created.</response>
        /// <response code="400">The Emergency Contact is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(EmergencyContact), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] EmergencyContact emergencyContact)
        {
            var result = await _emergencyContact.SaveAsync(emergencyContact);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return new ObjectResult(result.Resource) { StatusCode = 201 };
        }

        /// <summary>
        /// Update an existing Emergency Contact.
        /// </summary>
        /// <param name = "emergencyContact" > The Emergency Contact object.</param>
        /// <returns>The updated Emergency Contact.</returns>
        /// <response code = "200" > The Emergency Contact was successfully updated.</response>
        /// <response code = "400" > The Emergency Contact is invalid.</response>
        [HttpPut]
        [ProducesResponseType(typeof(EmergencyContact), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Update([FromBody] EmergencyContact emergencyContact)
        {
            var result = await _emergencyContact.Update(emergencyContact);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Delete a Emergency Contact.
        /// </summary>
        /// <param name="id">The Emergency Contact id.</param>
        /// <returns>The deleted Emergency Contact.</returns>
        /// <response code="200">The Emergency Contact was successfully deleted.</response>
        /// <response code="400">The Emergency Contact is invalid.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EmergencyContact), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _emergencyContact.Delete(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}
