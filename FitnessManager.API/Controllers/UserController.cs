using System.Threading.Tasks;
using FitnessManager.BusinessLogic.User.Interfaces;
using FitnessManager.Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="dto">UpdateUserDto</param>
        /// <param name="id">UserId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] string id, [FromBody] UpdateUserDto dto)
        {
            var entityUpdated = await _userService.UpdateAsync(id, dto);
            return HandleResponse(entityUpdated);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] string id)
        {
            var entityDeleted = await _userService.DeleteAsync(id);
            return HandleResponse(entityDeleted);
        }
    }
}