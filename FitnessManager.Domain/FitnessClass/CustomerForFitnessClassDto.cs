using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.FitnessClass
{
    public class CustomerForFitnessClassDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual AddressDto Address { get; set; }
        public virtual ContactDto Contact { get; set; }
    }
}