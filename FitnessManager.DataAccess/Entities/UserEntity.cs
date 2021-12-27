using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FitnessManager.DataAccess.Entities
{
    public class UserEntity : IdentityUser
    {
        [MaxLength(150)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }
        public Guid AddressId { get; set; }
        public virtual AddressEntity Address { get; set; }
        public Guid ContactId { get; set; }
        public virtual ContactEntity Contact { get; set; }
        public string Role { get; set; }
    }
}