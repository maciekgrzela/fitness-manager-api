using System.Collections.Generic;

namespace FitnessManager.Domain.SportsEquipment
{
    public class SportsEquipmentDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public HallForSportsEquipmentDto Hall { get; set; }
        public virtual ICollection<ReservationsForSportsEquipmentDto> EquipmentReservations { get; set; }
    }
}