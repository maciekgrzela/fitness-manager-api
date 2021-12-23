using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Membership;
using FitnessManager.Domain.User;
using FitnessManager.Domain.User.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class MembershipController : BaseController
    {
        /// <summary>
        /// Endpoint for user logging
        /// </summary>
        /// <param name="query">User Credentials</param>
        /// <returns>LoggedUserDto</returns>
        [ProducesResponseType(typeof(LoggedUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [HttpPost("/login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] Login.Query query)
        {
            var result = await Mediator.Send(query);
            return HandleResponse<LoggedUser, LoggedUserDto>(result);
        }
        
        /// <summary>
        /// Endpoint for user registering
        /// </summary>
        /// <param name="query">User Credentials</param>
        /// <returns>LoggedUserDto</returns>
        [ProducesResponseType(typeof(LoggedUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [HttpPost("/login")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] Register.Query query)
        {
            var result = await Mediator.Send(query);
            return HandleResponse<LoggedUser, LoggedUserDto>(result);
        }
        
        /// <summary>
        /// Endpoint for user registering
        /// </summary>
        /// <param name="query">User Credentials</param>
        /// <returns>LoggedUserDto</returns>
        [ProducesResponseType(typeof(LoggedUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [HttpGet("login/current")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var result = await Mediator.Send(new CurrentUser.Query());
            return HandleResponse<LoggedUser, LoggedUserDto>(result);
        }
    }
}