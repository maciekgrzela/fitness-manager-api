using FitnessManager.BusinessLogic.Instructor.Interfaces;

namespace FitnessManager.API.Controllers
{
    public class InstructorsController : BaseController
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }
    }
}