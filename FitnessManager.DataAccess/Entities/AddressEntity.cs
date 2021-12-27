using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace FitnessManager.DataAccess.Entities
{
    public class AddressEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public Guid? DepartmentId { get; set; }
        public virtual DepartmentEntity Department { get; set; }
        public Guid? FitnessClubId { get; set; }
        public virtual FitnessClubEntity FitnessClub { get; set; }
        public string UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}