using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.FitnessClub
{
    public class DepartmentForFitnessClubDto
    {
        public string Name { get; set; }
        public virtual AddressDto Address { get; set; }
        public virtual ContactDto Contact { get; set; }
    }
}