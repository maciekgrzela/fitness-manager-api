using System.Collections.Generic;

namespace FitnessManager.Domain.Hall
{
    public class HallDto
    {
        public int MaximumCapacity { get; set; }
        public virtual ICollection<SportsEquipmentForHallDto> SportsEquipments { get; set; }
        public virtual FitnessClubForHallDto FitnessClub { get; set; }
    }
}