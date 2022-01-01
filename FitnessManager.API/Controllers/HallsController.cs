using FitnessManager.BusinessLogic.Hall.Interfaces;

namespace FitnessManager.API.Controllers
{
    public class HallsController : BaseController
    {
        private readonly IHallService _hallService;

        public HallsController(IHallService hallService)
        {
            _hallService = hallService;
        }
    }
}