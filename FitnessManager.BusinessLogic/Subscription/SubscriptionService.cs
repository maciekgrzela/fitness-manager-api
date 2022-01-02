using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.Subscription.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.Subscription;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Subscription
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<SubscriptionEntity> _subscriptionRepository;

        public SubscriptionService(IUnitOfWork unitOfWork, IBaseRepository<SubscriptionEntity> subscriptionRepository)
        {
            _unitOfWork = unitOfWork;
            _subscriptionRepository = subscriptionRepository;
        }
        
        public async Task<IEnumerable<SubscriptionEntity>> GetAllAsync()
        {
            return await _subscriptionRepository.GetAll()
                .Include(p => p.Customers)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<SubscriptionEntity>> GetByIdAsync(Guid id)
        {
            var existingSubscription = await _subscriptionRepository.GetById(id)
                .Include(p => p.Customers)
                .FirstOrDefaultAsync();

            return existingSubscription == null
                ? BusinessLogicResponse<SubscriptionEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "Subscription with given id is not found")
                : BusinessLogicResponse<SubscriptionEntity>.Success(BusinessLogicResponseResult.Ok, existingSubscription);
        }

        public async Task<BusinessLogicResponse<SubscriptionEntity>> SaveAsync(SaveSubscriptionDto dto)
        {
            var subscription = new SubscriptionEntity
            {
                Name = dto.Name,
                Type = dto.Type,
                PeriodType = dto.Type,
                PricePerPeriod = dto.PricePerPeriod,
                Customers = new List<CustomerSubscriptionsEntity>()
            };

            await _subscriptionRepository.Add(subscription);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<SubscriptionEntity>.Success(BusinessLogicResponseResult.Created);
        }

        public async Task<BusinessLogicResponse<SubscriptionEntity>> UpdateAsync(Guid id, SaveSubscriptionDto dto)
        {
            var existingSubscription = await _subscriptionRepository.GetById(id).FirstOrDefaultAsync();

            if (existingSubscription == null)
            {
                return BusinessLogicResponse<SubscriptionEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "Subscription with given id is not found");
            }

            existingSubscription.Name = dto.Name;
            existingSubscription.Type = dto.Type;
            existingSubscription.PeriodType = dto.Type;
            existingSubscription.PricePerPeriod = dto.PricePerPeriod;

            _subscriptionRepository.Update(existingSubscription);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<SubscriptionEntity>.Success(BusinessLogicResponseResult.Updated);
        }

        public async Task<BusinessLogicResponse<SubscriptionEntity>> DeleteAsync(Guid id)
        {
            var existingSubscription = await _subscriptionRepository.GetById(id).FirstOrDefaultAsync();

            if (existingSubscription == null)
            {
                return BusinessLogicResponse<SubscriptionEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "Subscription with given id is not found");
            }

            await _subscriptionRepository.Delete(id);
            await _unitOfWork.CommitTransactionsAsync();

            return BusinessLogicResponse<SubscriptionEntity>.Success(BusinessLogicResponseResult.Deleted);
        }
    }
}