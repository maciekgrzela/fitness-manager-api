using System.Collections.Generic;
using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.FitnessClubNetwork
{
    public class FitnessClubNetworkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public  ICollection<FitnessClubsForFitnessClubNetworkDto> FitnessClubs { get; set; }
    }

    public class FitnessClubNetworkMapping : Profile
    {
        public FitnessClubNetworkMapping()
        {
            CreateMap<FitnessClubNetworkEntity, FitnessClubNetworkDto>();
            CreateMap<FitnessClubEntity, FitnessClubsForFitnessClubNetworkDto>();
        }
    }
}