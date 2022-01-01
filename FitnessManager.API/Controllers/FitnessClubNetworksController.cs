using FitnessManager.BusinessLogic.FitnessClubNetwork.Interfaces;

namespace FitnessManager.API.Controllers
{
    public class FitnessClubNetworksController : BaseController
    {
        private readonly IFitnessClubNetworkService _fitnessClubNetworkService;

        public FitnessClubNetworksController(IFitnessClubNetworkService fitnessClubNetworkService)
        {
            _fitnessClubNetworkService = fitnessClubNetworkService;
        }
    }
}