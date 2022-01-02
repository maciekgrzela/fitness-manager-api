using System.Collections.Generic;

namespace FitnessManager.Domain.FitnessClub
{
    public class HallForFitnessClubDto
    {
        public int MaximumCapacity { get; set; }
        public virtual ICollection<SportsEquipmentForFitnessClubDto> SportsEquipments { get; set; }
    }
}