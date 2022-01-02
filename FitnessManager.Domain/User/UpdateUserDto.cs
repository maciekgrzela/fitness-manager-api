using System;

namespace FitnessManager.Domain.User
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid AddressId { get; set; }
        public Guid ContactId { get; set; }
    }
}