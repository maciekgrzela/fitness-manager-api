namespace FitnessManager.Domain.SportsEquipment
{
    public class HallForSportsEquipmentDto
    {
        public int MaximumCapacity { get; set; }
        public virtual FitnessClubForSportsEquipmentDto FitnessClub { get; set; }
    }
}