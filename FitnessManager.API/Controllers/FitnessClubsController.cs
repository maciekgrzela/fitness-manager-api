using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.FitnessClub.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.FitnessClub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class FitnessClubsController : BaseController
    {
        private readonly IFitnessClubService _fitnessClubService;

        public FitnessClubsController(IFitnessClubService fitnessClubService)
        {
            _fitnessClubService = fitnessClubService;
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns>List of FitnessClubDto</returns>
        [ProducesResponseType(typeof(IEnumerable<FitnessClubDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entities = await _fitnessClubService.GetAllAsync();
            return OkDto<IEnumerable<FitnessClubEntity>, IEnumerable<FitnessClubDto>>(entities);
        }

        /// <summary>
        /// Get entity for given id
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>FitnessClubDto</returns>
        [ProducesResponseType(typeof(FitnessClubDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var entity = await _fitnessClubService.GetByIdAsync(id);
            return HandleResponse<FitnessClubEntity, FitnessClubDto>(entity);
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="dto">SaveFitnessClubDto</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveFitnessClubDto dto)
        {
            var entitySaved = await _fitnessClubService.SaveAsync(dto);
            return HandleResponse(entitySaved);
        }
        
        /// <summary>
        /// Assign department to fitness club
        /// </summary>
        /// <param name="dto">AssignDepartmentToFitnessClubDto</param>
        /// <param name="id">FitnessClubId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost("{id}/department")]
        public async Task<IActionResult> AssignDepartmentToFitnessClubAsync([FromQuery] Guid id, [FromBody] AssignDepartmentToFitnessClubDto dto)
        {
            var entitySaved = await _fitnessClubService.AssignDepartmentToFitnessClubAsync(id, dto);
            return HandleResponse(entitySaved);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="dto">UpdateFitnessClubDto</param>
        /// <param name="id">FitnessClubId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] SaveFitnessClubDto dto)
        {
            var entityUpdated = await _fitnessClubService.UpdateAsync(id, dto);
            return HandleResponse(entityUpdated);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">FitnessClubId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var entityDeleted = await _fitnessClubService.DeleteAsync(id);
            return HandleResponse(entityDeleted);
        }
    }
}