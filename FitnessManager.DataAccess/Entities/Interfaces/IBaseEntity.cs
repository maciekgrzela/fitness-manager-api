using System;

namespace FitnessManager.DataAccess.Entities.Interfaces
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}