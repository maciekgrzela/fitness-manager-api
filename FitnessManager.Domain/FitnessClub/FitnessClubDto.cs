using System.Collections.Generic;
using FitnessManager.Domain.Address;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.FitnessClub
{
    public class FitnessClubDto
    {
        public string Name { get; set; }
        public virtual AddressDto BaseAddress { get; set; }
        public virtual ContactDto BaseContact { get; set; }
        public virtual FitnessClubNetworkForFitnessClubDto FitnessClubNetwork { get; set; }
        public virtual ICollection<HallForFitnessClubDto> Halls { get; set; }
        public virtual ICollection<DepartmentForFitnessClubDto> Departments { get; set; }
    }
}