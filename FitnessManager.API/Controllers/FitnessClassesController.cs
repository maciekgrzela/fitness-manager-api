using FitnessManager.BusinessLogic.FitnessClass.Interfaces;

namespace FitnessManager.API.Controllers
{
    public class FitnessClassesController : BaseController
    {
        private readonly IFitnessClassService _fitnessClassService;

        public FitnessClassesController(IFitnessClassService fitnessClassService)
        {
            _fitnessClassService = fitnessClassService;
        }
    }
}