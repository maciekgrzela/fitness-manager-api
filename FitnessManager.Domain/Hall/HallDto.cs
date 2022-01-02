using System.Collections.Generic;
using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.Hall
{
    public class HallDto
    {
        public int MaximumCapacity { get; set; }
        public  ICollection<SportsEquipmentForHallDto> SportsEquipments { get; set; }
        public  FitnessClubForHallDto FitnessClub { get; set; }
    }

    public class HallMapping : Profile
    {
        public HallMapping()
        {
            CreateMap<HallEntity, HallDto>();
            CreateMap<SportsEquipmentEntity, SportsEquipmentForHallDto>();
            CreateMap<FitnessClubEntity, FitnessClubForHallDto>();
        }
    }
}