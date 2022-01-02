using System.Collections.Generic;
using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.Subscription
{
    public class SubscriptionDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string PeriodType { get; set; }
        public double PricePerPeriod { get; set; }
        public  ICollection<CustomerForSubscriptionDto> Customers { get; set; }
    }

    public class SubscriptionMapping : Profile
    {
        public SubscriptionMapping()
        {
            CreateMap<CustomerSubscriptionsEntity, CustomerForSubscriptionDto>();
        }
    }
}