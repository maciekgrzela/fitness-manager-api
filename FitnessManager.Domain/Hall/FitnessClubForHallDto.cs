using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.Hall
{
    public class FitnessClubForHallDto
    {
        public string Name { get; set; }
        public virtual AddressDto BaseAddress { get; set; }
        public virtual ContactDto BaseContact { get; set; }
    }
}