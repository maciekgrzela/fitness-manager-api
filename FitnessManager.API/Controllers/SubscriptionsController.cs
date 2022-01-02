using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Subscription.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Subscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class SubscriptionsController : BaseController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns>List of SubscriptionDto</returns>
        [ProducesResponseType(typeof(IEnumerable<SubscriptionDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entities = await _subscriptionService.GetAllAsync();
            return OkDto<IEnumerable<SubscriptionEntity>, IEnumerable<SubscriptionDto>>(entities);
        }

        /// <summary>
        /// Get entity for given id
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>SubscriptionDto</returns>
        [ProducesResponseType(typeof(SubscriptionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var entity = await _subscriptionService.GetByIdAsync(id);
            return HandleResponse<SubscriptionEntity, SubscriptionDto>(entity);
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="dto">SaveSubscriptionDto</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveSubscriptionDto dto)
        {
            var entitySaved = await _subscriptionService.SaveAsync(dto);
            return HandleResponse(entitySaved);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="dto">UpdateSubscriptionDto</param>
        /// <param name="id">SubscriptionId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] SaveSubscriptionDto dto)
        {
            var entityUpdated = await _subscriptionService.UpdateAsync(id, dto);
            return HandleResponse(entityUpdated);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">SubscriptionId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var entityDeleted = await _subscriptionService.DeleteAsync(id);
            return HandleResponse(entityDeleted);
        }
    }
}