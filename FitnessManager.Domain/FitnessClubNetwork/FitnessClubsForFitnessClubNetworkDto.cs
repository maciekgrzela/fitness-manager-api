using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.FitnessClubNetwork
{
    public class FitnessClubsForFitnessClubNetworkDto
    {
        public string Name { get; set; }
        public virtual AddressDto BaseAddress { get; set; }
        public virtual ContactDto BaseContact { get; set; }
    }
}