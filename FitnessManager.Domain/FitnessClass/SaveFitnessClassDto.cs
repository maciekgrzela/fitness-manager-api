using System;

namespace FitnessManager.Domain.FitnessClass
{
    public class SaveFitnessClassDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int MaximumParticipants { get; set; }
        public Guid? DefaultInstructorId { get; set; }
    }
}