namespace FitnessManager.Domain.Subscription
{
    public class SaveSubscriptionDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string PeriodType { get; set; }
        public double PricePerPeriod { get; set; }
    }
}