using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.FitnessClub
{
    public class DepartmentForFitnessClubDto
    {
        public string Name { get; set; }
        public  AddressDto Address { get; set; }
        public  ContactDto Contact { get; set; }
    }
}