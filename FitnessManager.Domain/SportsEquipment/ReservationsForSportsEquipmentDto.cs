using System;

namespace FitnessManager.Domain.SportsEquipment
{
    public class ReservationsForSportsEquipmentDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public CustomerForSportsEquipmentDto Customer { get; set; }
    }
}