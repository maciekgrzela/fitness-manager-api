using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Instructor.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Instructor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class InstructorsController : BaseController
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns>List of InstructorDto</returns>
        [ProducesResponseType(typeof(IEnumerable<InstructorDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entities = await _instructorService.GetAllAsync();
            return OkDto<IEnumerable<InstructorEntity>, IEnumerable<InstructorDto>>(entities);
        }

        /// <summary>
        /// Get entity for given id
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>InstructorDto</returns>
        [ProducesResponseType(typeof(InstructorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var entity = await _instructorService.GetByIdAsync(id);
            return HandleResponse<InstructorEntity, InstructorDto>(entity);
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="dto">SaveInstructorDto</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveInstructorDto dto)
        {
            var entitySaved = await _instructorService.SaveAsync(dto);
            return HandleResponse(entitySaved);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="dto">UpdateInstructorDto</param>
        /// <param name="id">InstructorId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] SaveInstructorDto dto)
        {
            var entityUpdated = await _instructorService.UpdateAsync(id, dto);
            return HandleResponse(entityUpdated);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">InstructorId</param>
        /// <returns>NoContent</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var entityDeleted = await _instructorService.DeleteAsync(id);
            return HandleResponse(entityDeleted);
        }
    }
}