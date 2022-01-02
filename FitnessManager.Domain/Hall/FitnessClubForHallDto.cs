using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.Hall
{
    public class FitnessClubForHallDto
    {
        public string Name { get; set; }
        public  AddressDto BaseAddress { get; set; }
        public  ContactDto BaseContact { get; set; }
    }
}