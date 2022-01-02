using System;
using FitnessManager.DataAccess.Entities.Interfaces;

namespace FitnessManager.DataAccess.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}