using System;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.Customer
{
    public class SportsEquipmentForCustomer
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public HallForCustomer Hall { get; set; }
    }
}