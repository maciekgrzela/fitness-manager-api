using System;
using System.Collections.Generic;
using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.Customer
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address.Address Address { get; set; }
        public Contact.Contact Contact { get; set; }
        public List<SubscriptionForCustomer> ActiveSubscriptions { get; set; }
        public List<FitnessClassEnrolmentsForCustomer> Enrolments { get; set; }
        public List<EquipmentReservationForCustomer> EquipmentReservations { get; set; }
    }

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerEntity, Customer>();
        }
    }
}