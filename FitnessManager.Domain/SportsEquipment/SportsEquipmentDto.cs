using System.Collections.Generic;
using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.SportsEquipment
{
    public class SportsEquipmentDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public HallForSportsEquipmentDto Hall { get; set; }
        public  ICollection<ReservationsForSportsEquipmentDto> EquipmentReservations { get; set; }
    }

    public class SportsEquipmentMapping : Profile
    {
        public SportsEquipmentMapping()
        {
            CreateMap<HallEntity, HallForSportsEquipmentDto>();
            CreateMap<EquipmentReservationEntity, ReservationsForSportsEquipmentDto>();
        }
    }
}