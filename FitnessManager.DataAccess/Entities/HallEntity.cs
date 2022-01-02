using System;
using System.Collections.Generic;

namespace FitnessManager.DataAccess.Entities
{
    public class HallEntity : BaseEntity
    {
        public int MaximumCapacity { get; set; }
        public virtual ICollection<SportsEquipmentEntity> SportsEquipments { get; set; }
        public Guid FitnessClubId { get; set; }
        public virtual FitnessClubEntity FitnessClub { get; set; }
    }
}