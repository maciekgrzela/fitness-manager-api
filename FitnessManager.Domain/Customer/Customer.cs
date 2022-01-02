using System;
using System.Collections.Generic;
using AutoMapper;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.Customer
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressDto Address { get; set; }
        public ContactDto Contact { get; set; }
        public List<SubscriptionForCustomer> ActiveSubscriptions { get; set; }
        public List<FitnessClassEnrolmentsForCustomer> Enrolments { get; set; }
        public List<EquipmentReservationForCustomer> EquipmentReservations { get; set; }
    }

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerEntity, Customer>();
            CreateMap<CustomerSubscriptionsEntity, SubscriptionForCustomer>();
            CreateMap<CustomerFitnessClassEnrolmentEntity, FitnessClassEnrolmentsForCustomer>();
            CreateMap<EquipmentReservationEntity, EquipmentReservationForCustomer>();
            CreateMap<SportsEquipmentEntity, SportsEquipmentForCustomer>();
            CreateMap<HallEntity, HallForCustomer>();
            CreateMap<InstructorEntity, InstructorForCustomer>();
            CreateMap<FitnessClassEntity, FitnessClassForCustomer>();
        }
    }
}