using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Hall.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Hall;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class HallsController : BaseController
    {
        private readonly IHallService _hallService;

        public HallsController(IHallService hallService)
        {
            _hallService = hallService;
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns>List of HallDto</returns>
        [ProducesResponseType(typeof(IEnumerable<HallDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entities = await _hallService.GetAllAsync();
            return OkDto<IEnumerable<HallEntity>, IEnumerable<HallDto>>(entities);
        }

        /// <summary>
        /// Get entity for given id
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>HallDto</returns>
        [ProducesResponseType(typeof(HallDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var entity = await _hallService.GetByIdAsync(id);
            return HandleResponse<HallEntity, HallDto>(entity);
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="dto">SaveHallDto</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveHallDto dto)
        {
            var entitySaved = await _hallService.SaveAsync(dto);
            return HandleResponse(entitySaved);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="dto">UpdateHallDto</param>
        /// <param name="id">HallId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] SaveHallDto dto)
        {
            var entityUpdated = await _hallService.UpdateAsync(id, dto);
            return HandleResponse(entityUpdated);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">HallId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var entityDeleted = await _hallService.DeleteAsync(id);
            return HandleResponse(entityDeleted);
        }
    }
}