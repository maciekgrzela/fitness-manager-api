﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessManager.DataAccess.Entities
{
    public class FitnessClubEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BaseAddressId { get; set; }
        public virtual AddressEntity BaseAddress { get; set; }
        public Guid BaseContactId { get; set; }
        public virtual ContactEntity BaseContact { get; set; }
        public Guid? FitnessClubNetworkId { get; set; }
        public virtual FitnessClubNetworkEntity FitnessClubNetwork { get; set; }
        public virtual ICollection<HallEntity> Halls { get; set; }
        public virtual ICollection<DepartmentEntity> Departments { get; set; }
    }
}