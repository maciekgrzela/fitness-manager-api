using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.User
{
    public class LoggedUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public AddressDto Address { get; set; }
        public ContactDto Contact { get; set; }
        public string Token { get; set; }
    }
}