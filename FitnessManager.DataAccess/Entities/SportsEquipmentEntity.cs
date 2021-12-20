using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessManager.DataAccess.Entities
{
    public class SportsEquipmentEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public Guid? HallId { get; set; }
        public HallEntity Hall { get; set; }
        public virtual ICollection<EquipmentReservationEntity> EquipmentReservations { get; set; }
    }
}