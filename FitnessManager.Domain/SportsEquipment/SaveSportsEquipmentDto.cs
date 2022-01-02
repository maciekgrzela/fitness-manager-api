using System;

namespace FitnessManager.Domain.SportsEquipment
{
    public class SaveSportsEquipmentDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public Guid HallId { get; set; }
    }
}