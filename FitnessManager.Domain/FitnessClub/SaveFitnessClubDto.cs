using System;

namespace FitnessManager.Domain.FitnessClub
{
    public class SaveFitnessClubDto
    {
        public string Name { get; set; }
        public Guid BaseAddressId { get; set; }
        public Guid BaseContactId { get; set; }
        public Guid? FitnessClubNetworkId { get; set; }
    }
}