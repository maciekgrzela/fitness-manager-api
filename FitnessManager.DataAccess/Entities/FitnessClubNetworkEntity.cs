using System.Collections.Generic;

namespace FitnessManager.DataAccess.Entities
{
    public class FitnessClubNetworkEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public virtual ICollection<FitnessClubEntity> FitnessClubs { get; set; }
    }
}