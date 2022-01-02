using System;
using System.Collections.Generic;
using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.Customer
{
    public class SaveCustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SaveAddressDto Address { get; set; }
        public SaveContactDto Contact { get; set; }
        public ICollection<Guid> ActiveSubscriptions { get; set; }
        public virtual ICollection<Guid> Enrolments { get; set; }
    }
}