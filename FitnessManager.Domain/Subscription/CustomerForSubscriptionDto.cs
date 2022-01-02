using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.Subscription
{
    public class CustomerForSubscriptionDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  AddressDto Address { get; set; }
        public  ContactDto Contact { get; set; }
    }
}