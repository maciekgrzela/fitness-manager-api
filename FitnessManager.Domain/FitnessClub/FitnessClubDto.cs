using System.Collections.Generic;
using AutoMapper;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.FitnessClub
{
    public class FitnessClubDto
    {
        public string Name { get; set; }
        public  AddressDto BaseAddress { get; set; }
        public  ContactDto BaseContact { get; set; }
        public  FitnessClubNetworkForFitnessClubDto FitnessClubNetwork { get; set; }
        public  ICollection<HallForFitnessClubDto> Halls { get; set; }
        public  ICollection<DepartmentForFitnessClubDto> Departments { get; set; }
    }

    public class FitnessClubMapping : Profile
    {
        public FitnessClubMapping()
        {
            CreateMap<FitnessClubEntity, FitnessClubDto>();
            CreateMap<SaveFitnessClubDto, FitnessClubEntity>();
            CreateMap<FitnessClubNetworkEntity, FitnessClubNetworkForFitnessClubDto>();
            CreateMap<HallEntity, HallForFitnessClubDto>();
            CreateMap<DepartmentEntity, DepartmentForFitnessClubDto>();
        }
    }
}