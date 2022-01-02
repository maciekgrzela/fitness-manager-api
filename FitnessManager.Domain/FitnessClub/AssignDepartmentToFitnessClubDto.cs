using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.FitnessClub
{
    public class AssignDepartmentToFitnessClubDto
    {
        public string Name { get; set; }
        public SaveAddressDto Address { get; set; }
        public SaveContactDto Contact { get; set; }
    }
}