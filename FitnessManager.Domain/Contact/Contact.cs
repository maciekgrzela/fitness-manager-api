using System;
using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.Contact
{
    public class Contact
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
            CreateMap<SaveContactDto, Contact>();
            CreateMap<ContactEntity, Contact>();
            CreateMap<Contact, ContactEntity>();
            CreateMap<Contact, ContactDto>();
        }
    }
}