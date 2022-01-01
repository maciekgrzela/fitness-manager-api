using FitnessManager.BusinessLogic.User.Interfaces;

namespace FitnessManager.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}