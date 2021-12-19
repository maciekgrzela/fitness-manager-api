using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Membership;
using FitnessManager.Domain.User;
using FitnessManager.Domain.User.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class MembershipController : BaseController
    {
        [HttpPost("/login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] Login.Query query)
        {
            var result = await Mediator.Send(query);
            return HandleResponse<LoggedUser, LoggedUserDto>(result);
        }
    }
}