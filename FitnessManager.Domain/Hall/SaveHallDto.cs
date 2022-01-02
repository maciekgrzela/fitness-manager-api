using System;

namespace FitnessManager.Domain.Hall
{
    public class SaveHallDto
    {
        public int MaximumCapacity { get; set; }
        public virtual Guid FitnessClubId { get; set; }
    }
}