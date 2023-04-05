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
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Get all Reservations.
        /// </summary>
        /// <param name="filter">The pagination filter</param>
        /// <returns>The list of Reservations</returns>
        /// <response code="200">The Reservations were successfully retrieved.</response> 
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<Reservation>>), 200)]
        public async Task<PagedResponse<IEnumerable<Reservation>>> ListAsync([FromQuery] PaginationParameter filter)
        {
            var validFilter = new PaginationParameter(filter.PageNumber, filter.PageSize);
            return await _reservationService.ListAsync(validFilter);
        }

        /// <summary>
        /// Get a Reservation by id.
        /// </summary>
        /// <returns>The Reservation.</returns>
        /// <response code="200">The Reservation was successfully retrieved.</response> 
        /// <response code="404">The Reservation does not exist.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Reservation), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            var result = await _reservationService.FindByIdAsync(id);

            if (!result.Success)
            {
                return new ObjectResult(new ErrorResource(result.Message)) { StatusCode = 404 };
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Create a new Reservation.
        /// </summary>
        /// <param name="reservation">The Reservation object.</param>
        /// <returns>The created Reservation.</returns>
        /// <response code="201">The Reservation was successfully created.</response>
        /// <response code="400">The Reservation is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Reservation), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] Reservation reservation)
        {
            var result = await _reservationService.SaveAsync(reservation);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return new ObjectResult(result.Resource) { StatusCode = 201 };
        }

        /// <summary>
        /// Update an existing Reservation.
        /// </summary>
        /// <param name = "reservation" > The Reservation object.</param>
        /// <returns>The updated Reservation.</returns>
        /// <response code = "200" > The Reservation was successfully updated.</response>
        /// <response code = "400" > The Reservation is invalid.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Reservation), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Update([FromBody] Reservation reservation)
        {
            var result = await _reservationService.Update(reservation);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Delete a Reservation.
        /// </summary>
        /// <param name="id">The Reservation id.</param>
        /// <returns>The deleted Reservation.</returns>
        /// <response code="200">The Reservation was successfully deleted.</response>
        /// <response code="400">The Reservation is invalid.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Reservation), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _reservationService.Delete(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}
