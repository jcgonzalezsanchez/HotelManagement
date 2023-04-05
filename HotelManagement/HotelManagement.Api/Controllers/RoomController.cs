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
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Get all Rooms.
        /// </summary>
        /// <param name="filter">The pagination filter</param>
        /// <returns>The list of Rooms</returns>
        /// <response code="200">The Rooms were successfully retrieved.</response> 
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<Room>>), 200)]
        public async Task<PagedResponse<IEnumerable<Room>>> ListAsync([FromQuery] PaginationParameter filter)
        {
            var validFilter = new PaginationParameter(filter.PageNumber, filter.PageSize);
            return await _roomService.ListAsync(validFilter);
        }

        /// <summary>
        /// Get a Room by id.
        /// </summary>
        /// <returns>The Room.</returns>
        /// <response code="200">The Room was successfully retrieved.</response> 
        /// <response code="404">The Room does not exist.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Room), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            var result = await _roomService.FindByIdAsync(id);

            if (!result.Success)
            {
                return new ObjectResult(new ErrorResource(result.Message)) { StatusCode = 404 };
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Create a new Room.
        /// </summary>
        /// <param name="room">The Room object.</param>
        /// <returns>The created Room.</returns>
        /// <response code="201">The Room was successfully created.</response>
        /// <response code="400">The Room is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Room), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] Room room)
        {
            var result = await _roomService.SaveAsync(room);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return new ObjectResult(result.Resource) { StatusCode = 201 };
        }

        /// <summary>
        /// Update an existing Room.
        /// </summary>
        /// <param name = "room" > The Room object.</param>
        /// <returns>The updated Room.</returns>
        /// <response code = "200" > The Room was successfully updated.</response>
        /// <response code = "400" > The Room is invalid.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Room), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Update([FromBody] Room room)
        {
            var result = await _roomService.Update(room);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Delete a Room.
        /// </summary>
        /// <param name="id">The Room id.</param>
        /// <returns>The deleted Room.</returns>
        /// <response code="200">The Room was successfully deleted.</response>
        /// <response code="400">The Room is invalid.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Room), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roomService.Delete(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}
