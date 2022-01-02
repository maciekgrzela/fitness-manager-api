using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.FitnessClubNetwork
{
    public class FitnessClubsForFitnessClubNetworkDto
    {
        public string Name { get; set; }
        public  AddressDto BaseAddress { get; set; }
        public  ContactDto BaseContact { get; set; }
    }
}