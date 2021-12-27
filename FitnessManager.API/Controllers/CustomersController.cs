using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Customer;
using FitnessManager.Domain.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class CustomersController : BaseController
    {
        /// <summary>
        /// Get list of customers
        /// </summary>
        /// <returns>List of CustomerDto</returns>
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await Mediator.Send(new GetAll.Query());
            return HandleResponse(result);
        }
        
        /// <summary>
        /// Get customer for given id
        /// </summary>
        /// <param name="id">CustomerId</param>
        /// <returns>CustomerDto</returns>
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var result = await Mediator.Send(new Get.Query { Id = id });
            return HandleResponse(result);
        }
        
        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="body">CreateCustomer.Command</param>
        /// <returns>Empty content</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] Create.Command command)
        {
            var result = await Mediator.Send(command);
            return HandleResponse<Customer>(result);
        }
        
        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="body">UpdateCustomer.Command</param>
        /// <param name="id">CustomerId</param>
        /// <returns>Empty content</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] Update.Command command)
        {
            command.SetId(id);
            var result = await Mediator.Send(command);
            return HandleResponse<Customer>(result);
        }
        
        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="id">CustomerId</param>
        /// <returns>Empty content</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var result = await Mediator.Send(new Delete.Command { Id = id });
            return HandleResponse<Customer>(result);
        }
    }
}