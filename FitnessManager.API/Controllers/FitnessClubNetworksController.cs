using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.FitnessClubNetwork.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.FitnessClubNetwork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class FitnessClubNetworksController : BaseController
    {
        private readonly IFitnessClubNetworkService _fitnessClubNetworkService;

        public FitnessClubNetworksController(IFitnessClubNetworkService fitnessClubNetworkService)
        {
            _fitnessClubNetworkService = fitnessClubNetworkService;
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns>List of FitnessClubNetworkDto</returns>
        [ProducesResponseType(typeof(IEnumerable<FitnessClubNetworkDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entities = await _fitnessClubNetworkService.GetAllAsync();
            return OkDto<IEnumerable<FitnessClubNetworkEntity>, IEnumerable<FitnessClubNetworkDto>>(entities);
        }

        /// <summary>
        /// Get entity for given id
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>FitnessClubNetworkDto</returns>
        [ProducesResponseType(typeof(FitnessClubNetworkDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var entity = await _fitnessClubNetworkService.GetByIdAsync(id);
            return HandleResponse<FitnessClubNetworkEntity, FitnessClubNetworkDto>(entity);
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="dto">SaveFitnessClubNetworkDto</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveFitnessClubNetworkDto dto)
        {
            var entitySaved = await _fitnessClubNetworkService.SaveAsync(dto);
            return HandleResponse(entitySaved);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="dto">UpdateFitnessClubNetworkDto</param>
        /// <param name="id">FitnessClubNetworkId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] SaveFitnessClubNetworkDto dto)
        {
            var entityUpdated = await _fitnessClubNetworkService.UpdateAsync(id, dto);
            return HandleResponse(entityUpdated);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">FitnessClubNetworkId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var entityDeleted = await _fitnessClubNetworkService.DeleteAsync(id);
            return HandleResponse(entityDeleted);
        }
    }   
    
}