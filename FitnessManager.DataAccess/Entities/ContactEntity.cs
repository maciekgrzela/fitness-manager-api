using System;

namespace FitnessManager.DataAccess.Entities
{
    public class ContactEntity
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public Guid CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual DepartmentEntity Department { get; set; }
        public Guid FitnessClubId { get; set; }
        public virtual FitnessClubEntity FitnessClub { get; set; }
        public Guid InstructorId { get; set; }
        public virtual InstructorEntity Instructor { get; set; }
        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}