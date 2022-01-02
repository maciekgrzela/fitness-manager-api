using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Subscription;

namespace FitnessManager.BusinessLogic.Subscription.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionEntity>> GetAllAsync();
        Task<BusinessLogicResponse<SubscriptionEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<SubscriptionEntity>> SaveAsync(SaveSubscriptionDto dto);
        Task<BusinessLogicResponse<SubscriptionEntity>> UpdateAsync(Guid id, SaveSubscriptionDto dto);
        Task<BusinessLogicResponse<SubscriptionEntity>> DeleteAsync(Guid id);
    }
}