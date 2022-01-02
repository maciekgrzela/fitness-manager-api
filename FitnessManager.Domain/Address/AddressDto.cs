using System;
using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.Address
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
    
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<SaveAddressDto, AddressDto>();
            CreateMap<AddressEntity, AddressDto>();
            CreateMap<AddressDto, AddressEntity>();
        }
    }
}