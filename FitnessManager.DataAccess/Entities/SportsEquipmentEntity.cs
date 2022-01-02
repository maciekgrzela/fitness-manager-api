using System;
using System.Collections.Generic;

namespace FitnessManager.DataAccess.Entities
{
    public class SportsEquipmentEntity : BaseEntity
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public Guid? HallId { get; set; }
        public HallEntity Hall { get; set; }
        public virtual ICollection<EquipmentReservationEntity> EquipmentReservations { get; set; }
    }
}