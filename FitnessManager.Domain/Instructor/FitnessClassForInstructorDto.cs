namespace FitnessManager.Domain.Instructor
{
    public class FitnessClassForInstructorDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int MaximumParticipants { get; set; }
    }
}