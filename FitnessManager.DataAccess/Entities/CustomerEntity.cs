using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessManager.DataAccess.Entities
{
    public class CustomerEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid AddressId { get; set; }
        public virtual AddressEntity Address { get; set; }
        public Guid ContactId { get; set; }
        public virtual ContactEntity Contact { get; set; }
        public virtual ICollection<CustomerSubscriptionsEntity> ActiveSubscriptions { get; set; }
        public virtual ICollection<CustomerFitnessClassEnrolmentEntity> Enrolments { get; set; }
        public virtual ICollection<EquipmentReservationEntity> EquipmentReservations { get; set; }
    }
}