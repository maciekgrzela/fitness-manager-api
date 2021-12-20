﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessManager.DataAccess.Entities
{
    public class EquipmentReservationEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid SportsEquipmentId { get; set; }
        public virtual SportsEquipmentEntity SportsEquipment { get; set; }
        public Guid CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
    }
}