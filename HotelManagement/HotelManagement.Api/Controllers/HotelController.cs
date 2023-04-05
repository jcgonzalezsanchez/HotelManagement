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
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        /// <summary>
        /// Get all Hotels.
        /// </summary>
        /// <param name="filter">The pagination filter</param>
        /// <returns>The list of Hotels</returns>
        /// <response code="200">The Hotels were successfully retrieved.</response> 
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<Hotel>>), 200)]
        public async Task<PagedResponse<IEnumerable<Hotel>>> ListAsync([FromQuery] PaginationParameter filter)
        {
            var validFilter = new PaginationParameter(filter.PageNumber, filter.PageSize);
            return await _hotelService.ListAsync(validFilter);
        }

        /// <summary>
        /// Get a Hotel by id.
        /// </summary>
        /// <returns>The Hotel.</returns>
        /// <response code="200">The Hotel was successfully retrieved.</response> 
        /// <response code="404">The Hotel does not exist.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Hotel), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            var result = await _hotelService.FindByIdAsync(id);

            if (!result.Success)
            {
                return new ObjectResult(new ErrorResource(result.Message)) { StatusCode = 404 };
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Create a new Hotel.
        /// </summary>
        /// <param name="hotel">The Hotel object.</param>
        /// <returns>The created Hotel.</returns>
        /// <response code="201">The Hotel was successfully created.</response>
        /// <response code="400">The Hotel is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Hotel), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] Hotel hotel)
        {
            var result = await _hotelService.SaveAsync(hotel);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return new ObjectResult(result.Resource) { StatusCode = 201 };
        }

        /// <summary>
        /// Update an existing Hotel.
        /// </summary>
        /// <param name = "hotel" > The Hotel object.</param>
        /// <returns>The updated Hotel.</returns>
        /// <response code = "200" > The Hotel was successfully updated.</response>
        /// <response code = "400" > The Hotel is invalid.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Hotel), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Update([FromBody] Hotel hotel)
        {
            var result = await _hotelService.Update(hotel);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Delete a Hotel.
        /// </summary>
        /// <param name="id">The Hotel id.</param>
        /// <returns>The deleted Hotel.</returns>
        /// <response code="200">The Hotel was successfully deleted.</response>
        /// <response code="400">The Hotel is invalid.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Hotel), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _hotelService.Delete(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}
