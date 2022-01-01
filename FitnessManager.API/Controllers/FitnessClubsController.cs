using FitnessManager.BusinessLogic.FitnessClub.Interfaces;

namespace FitnessManager.API.Controllers
{
    public class FitnessClubsController : BaseController
    {
        private readonly IFitnessClubService _fitnessClubService;

        public FitnessClubsController(IFitnessClubService fitnessClubService)
        {
            _fitnessClubService = fitnessClubService;
        }
    }
}