using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessManager.DataAccess.Entities
{
    public class DepartmentEntity : BaseEntity
    {
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        public virtual AddressEntity Address { get; set; }
        public Guid ContactId { get; set; }
        public virtual ContactEntity Contact { get; set; }
        public Guid FitnessClubId { get; set; }
        public virtual FitnessClubEntity FitnessClub { get; set; }
    }
}