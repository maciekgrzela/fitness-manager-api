using FitnessManager.BusinessLogic.Subscription.Interfaces;

namespace FitnessManager.API.Controllers
{
    public class SubscriptionsController : BaseController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
    }
}