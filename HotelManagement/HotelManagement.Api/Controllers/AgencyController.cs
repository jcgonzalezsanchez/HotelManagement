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
    public class AgencyController : Controller
    {
        private readonly IAgencyService _agencyService;

        public AgencyController(IAgencyService agencyService)
        {
            _agencyService = agencyService;
        }

        /// <summary>
        /// Get all Agencies.
        /// </summary>
        /// <param name="filter">The pagination filter</param>
        /// <returns>The list of Agencies</returns>
        /// <response code="200">The Agencies were successfully retrieved.</response> 
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<Agency>>), 200)]
        public async Task<PagedResponse<IEnumerable<Agency>>> ListAsync([FromQuery] PaginationParameter filter)
        {
            var validFilter = new PaginationParameter(filter.PageNumber, filter.PageSize);
            return await _agencyService.ListAsync(validFilter);
        }

        /// <summary>
        /// Get a Agency by id.
        /// </summary>
        /// <returns>The Agency.</returns>
        /// <response code="200">The Agency was successfully retrieved.</response> 
        /// <response code="404">The Agency does not exist.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Agency), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            var result = await _agencyService.FindByIdAsync(id);

            if (!result.Success)
            {
                return new ObjectResult(new ErrorResource(result.Message)) { StatusCode = 404 };
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Create a new Agency.
        /// </summary>
        /// <param name="agency">The Agency object.</param>
        /// <returns>The created Agency.</returns>
        /// <response code="201">The Agency was successfully created.</response>
        /// <response code="400">The Agency is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Agency), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] Agency agency)
        {
            var result = await _agencyService.SaveAsync(agency);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return new ObjectResult(result.Resource) { StatusCode = 201 };
        }

        /// <summary>
        /// Update an existing Agency.
        /// </summary>
        /// <param name = "agency" > The Agency object.</param>
        /// <returns>The updated Agency.</returns>
        /// <response code = "200" > The Agency was successfully updated.</response>
        /// <response code = "400" > The Agency is invalid.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Agency), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Update([FromBody] Agency agency)
        {
            var result = await _agencyService.Update(agency);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Delete a Agency.
        /// </summary>
        /// <param name="id">The Agency id.</param>
        /// <returns>The deleted Agency.</returns>
        /// <response code="200">The Agency was successfully deleted.</response>
        /// <response code="400">The Agency is invalid.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Agency), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _agencyService.Delete(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}
