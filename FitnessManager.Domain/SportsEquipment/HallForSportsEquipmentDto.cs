namespace FitnessManager.Domain.SportsEquipment
{
    public class HallForSportsEquipmentDto
    {
        public int MaximumCapacity { get; set; }
        public  FitnessClubForSportsEquipmentDto FitnessClub { get; set; }
    }
}