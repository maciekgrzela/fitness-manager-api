using System;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.Customer
{
    public class EquipmentReservationForCustomer
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public SportsEquipmentForCustomer SportsEquipment { get; set; }
    }
}