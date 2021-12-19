using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FitnessManager.DataAccess.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        [MaxLength(150)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}