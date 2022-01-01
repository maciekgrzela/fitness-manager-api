using FitnessManager.BusinessLogic.SportsEquipment.Interfaces;

namespace FitnessManager.API.Controllers
{
    public class SportEquipmentsController : BaseController
    {
        private readonly ISportsEquipmentService _sportsEquipmentService;

        public SportEquipmentsController(ISportsEquipmentService sportsEquipmentService)
        {
            _sportsEquipmentService = sportsEquipmentService;
        }
    }
}