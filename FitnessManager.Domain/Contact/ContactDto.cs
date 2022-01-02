using System;
using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.Contact
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
    }
    
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<SaveContactDto, ContactDto>();
            CreateMap<ContactEntity, ContactDto>();
            CreateMap<ContactDto, ContactEntity>();
        }
    }
}