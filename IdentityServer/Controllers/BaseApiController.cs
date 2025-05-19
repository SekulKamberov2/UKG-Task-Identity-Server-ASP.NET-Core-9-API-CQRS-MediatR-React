namespace IdentityServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using IdentityServer.Application.Results;

    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult AsActionResult<T>(IdentityResult<T> result)
        {
            if (result == null)
                return NotFound("The requested result was not found.");

            // Success case
            if (result.IsSuccess)
            {
                if (result.Data == null) // No data available, but the operation was successful (DELETE)
                    return NoContent(); // 204 No Content

                return Ok(result); // 200 OK with content
            }
             
            if (!string.IsNullOrWhiteSpace(result.Error))
            { 
                if (result.StatusCode.HasValue)  
                    return StatusCode(result.StatusCode.Value, result.Error); 
                 
                if (result.Error.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                    return Conflict(result.Error); // 409 conflict

                if (result.Error.Contains("not found", StringComparison.OrdinalIgnoreCase))
                    return NotFound(result.Error); // 404 not Found

                if (result.Error.Contains("unauthorized", StringComparison.OrdinalIgnoreCase))
                    return Unauthorized(result.Error); // 401 unauthorized

                if (result.Error.Contains("unexpected", StringComparison.OrdinalIgnoreCase))
                    return StatusCode(StatusCodes.Status500InternalServerError, result.Error); // 500 internal Server Error

                return BadRequest(result.Error); // 400 Bad Request for general validation failures
            }

            return BadRequest("An unknown error occurred."); // default failure case
        }
    }
}
