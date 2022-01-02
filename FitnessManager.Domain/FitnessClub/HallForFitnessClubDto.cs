using System.Collections.Generic;

namespace FitnessManager.Domain.FitnessClub
{
    public class HallForFitnessClubDto
    {
        public int MaximumCapacity { get; set; }
        public  ICollection<SportsEquipmentForFitnessClubDto> SportsEquipments { get; set; }
    }
}