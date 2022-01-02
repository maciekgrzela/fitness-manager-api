using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.FitnessClass.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.FitnessClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class FitnessClassesController : BaseController
    {
        
        private readonly IFitnessClassService _fitnessClassService;

        public FitnessClassesController(IFitnessClassService fitnessClassService)
        {
            _fitnessClassService = fitnessClassService;
        }
        
        /// <summary>
        /// Get list of fitness classes
        /// </summary>
        /// <returns>List of FitnessClassDto</returns>
        [ProducesResponseType(typeof(IEnumerable<FitnessClassDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _fitnessClassService.GetAllAsync();
            return OkDto<IEnumerable<FitnessClassEntity>, IEnumerable<FitnessClassDto>>(customers);
        }
        
        /// <summary>
        /// Get fitness class for given id
        /// </summary>
        /// <param name="id">FitnessClassId</param>
        /// <returns>FitnessClassDto</returns>
        [ProducesResponseType(typeof(FitnessClassDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var fitnessClass = await _fitnessClassService.GetByIdAsync(id);
            return HandleResponse<FitnessClassEntity, FitnessClassDto>(fitnessClass);
        }
        
        /// <summary>
        /// Create new fitness class
        /// </summary>
        /// <param name="dto">SaveFitnessClassDto</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveFitnessClassDto dto)
        {
            var fitnessClassSaved = await _fitnessClassService.SaveAsync(dto);
            return HandleResponse(fitnessClassSaved);
        }
        
        /// <summary>
        /// Update fitness club
        /// </summary>
        /// <param name="dto">UpdateFitnessClassDto</param>
        /// <param name="id">FitnessClassId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] SaveFitnessClassDto dto)
        {
            var fitnessClassUpdated = await _fitnessClassService.UpdateAsync(id, dto);
            return HandleResponse(fitnessClassUpdated);
        }
        
        /// <summary>
        /// Delete fitness class
        /// </summary>
        /// <param name="id">FitnessClassId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var fitnessClassDeleted = await _fitnessClassService.DeleteAsync(id);
            return HandleResponse(fitnessClassDeleted);
        }
    }
}