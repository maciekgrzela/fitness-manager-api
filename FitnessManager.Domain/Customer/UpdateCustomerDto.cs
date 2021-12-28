using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.Customer
{
    public class UpdateCustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SaveAddressDto Address { get; set; }
        public SaveContactDto Contact { get; set; }
    }
}