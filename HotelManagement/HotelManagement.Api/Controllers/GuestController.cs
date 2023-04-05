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
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        /// <summary>
        /// Get all Guests.
        /// </summary>
        /// <param name="filter">The pagination filter</param>
        /// <returns>The list of Guests</returns>
        /// <response code="200">The Guests were successfully retrieved.</response> 
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<Guest>>), 200)]
        public async Task<PagedResponse<IEnumerable<Guest>>> ListAsync([FromQuery] PaginationParameter filter)
        {
            var validFilter = new PaginationParameter(filter.PageNumber, filter.PageSize);
            return await _guestService.ListAsync(validFilter);
        }

        /// <summary>
        /// Get a Guest by id.
        /// </summary>
        /// <returns>The Guest.</returns>
        /// <response code="200">The Guest was successfully retrieved.</response> 
        /// <response code="404">The Guest does not exist.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Guest), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            var result = await _guestService.FindByIdAsync(id);

            if (!result.Success)
            {
                return new ObjectResult(new ErrorResource(result.Message)) { StatusCode = 404 };
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Create a new Guest.
        /// </summary>
        /// <param name="guest">The Guest object.</param>
        /// <returns>The created Guest.</returns>
        /// <response code="201">The Guest was successfully created.</response>
        /// <response code="400">The Guest is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guest), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] Guest guest)
        {
            var result = await _guestService.SaveAsync(guest);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return new ObjectResult(result.Resource) { StatusCode = 201 };
        }

        /// <summary>
        /// Update an existing Guest.
        /// </summary>
        /// <param name = "guest" > The Guest object.</param>
        /// <returns>The updated Guest.</returns>
        /// <response code = "200" > The Guest was successfully updated.</response>
        /// <response code = "400" > The Guest is invalid.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Guest), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Update([FromBody] Guest guest)
        {
            var result = await _guestService.Update(guest);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Delete a Guest.
        /// </summary>
        /// <param name="id">The Guest id.</param>
        /// <returns>The deleted Guest.</returns>
        /// <response code="200">The Guest was successfully deleted.</response>
        /// <response code="400">The Guest is invalid.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Guest), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _guestService.Delete(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}
