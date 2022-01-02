using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.FitnessClass
{
    public class InstructorForFitnessClassDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ContactEntity Contact { get; set; }
    }
}