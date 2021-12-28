using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Customer.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        /// <summary>
        /// Get list of customers
        /// </summary>
        /// <returns>List of CustomerDto</returns>
        [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _customerService.GetAllAsync();
            return OkDto<IEnumerable<CustomerEntity>, IEnumerable<CustomerDto>>(customers);
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
            var customer = await _customerService.GetByIdAsync(id);
            return HandleResponse<CustomerEntity, CustomerDto>(customer);
        }
        
        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="body">CreateCustomer.Command</param>
        /// <returns>Empty content</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveCustomerDto saveCustomerDto)
        {
            var customerSaved = await _customerService.SaveAsync(saveCustomerDto);
            return HandleResponse(customerSaved);
        }
        
        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="body">UpdateCustomer.Command</param>
        /// <param name="id">CustomerId</param>
        /// <returns>Empty content</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            var customerUpdated = await _customerService.UpdateAsync(id, updateCustomerDto);
            return HandleResponse(customerUpdated);
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
            var customerDeleted = await _customerService.DeleteAsync(id);
            return HandleResponse(customerDeleted);
        }
    }
}