using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.SportsEquipment.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.SportsEquipment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class SportsEquipmentController : BaseController
    {
        private readonly ISportsEquipmentService _sportsEquipmentService;

        public SportsEquipmentController(ISportsEquipmentService sportsEquipmentService)
        {
            _sportsEquipmentService = sportsEquipmentService;
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns>List of SportsEquipmentDto</returns>
        [ProducesResponseType(typeof(IEnumerable<SportsEquipmentDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entities = await _sportsEquipmentService.GetAllAsync();
            return OkDto<IEnumerable<SportsEquipmentEntity>, IEnumerable<SportsEquipmentDto>>(entities);
        }

        /// <summary>
        /// Get entity for given id
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>SportsEquipmentDto</returns>
        [ProducesResponseType(typeof(SportsEquipmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var entity = await _sportsEquipmentService.GetByIdAsync(id);
            return HandleResponse<SportsEquipmentEntity, SportsEquipmentDto>(entity);
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="dto">SaveSportsEquipmentDto</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveSportsEquipmentDto dto)
        {
            var entitySaved = await _sportsEquipmentService.SaveAsync(dto);
            return HandleResponse(entitySaved);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="dto">UpdateSportsEquipmentDto</param>
        /// <param name="id">SportsEquipmentId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] SaveSportsEquipmentDto dto)
        {
            var entityUpdated = await _sportsEquipmentService.UpdateAsync(id, dto);
            return HandleResponse(entityUpdated);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">SportsEquipmentId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var entityDeleted = await _sportsEquipmentService.DeleteAsync(id);
            return HandleResponse(entityDeleted);
        }
    }
}