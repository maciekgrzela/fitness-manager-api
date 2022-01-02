using System.Collections.Generic;

namespace FitnessManager.Domain.FitnessClubNetwork
{
    public class FitnessClubNetworkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public virtual ICollection<FitnessClubsForFitnessClubNetworkDto> FitnessClubs { get; set; }
    }
}