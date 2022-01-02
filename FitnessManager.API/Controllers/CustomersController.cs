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
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _customerService.GetAllAsync();
            return OkDto<IEnumerable<CustomerEntity>, IEnumerable<Customer>>(customers);
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
            return HandleResponse<CustomerEntity, Customer>(customer);
        }
        
        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="dto">CreateCustomer.Command</param>
        /// <returns>Empty content</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveCustomerDto dto)
        {
            var customerSaved = await _customerService.SaveAsync(dto);
            return HandleResponse(customerSaved);
        }
        
        /// <summary>
        /// Enrol customer to fitness classes
        /// </summary>
        /// <param name="id">CustomerId</param>
        /// <param name="enrolmentId">EnrolmentId</param>
        /// <returns>Empty content</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPost("{id}/enrolments/{enrolmentId}")]
        public async Task<IActionResult> SaveCustomerToFitnessClassEnrolmentAsync(Guid id, Guid enrolmentId)
        {
            var customerToFitnessClassEnrolment = await _customerService.EnrolCustomerToFitnessClassAsync(id, enrolmentId);
            return HandleResponse(customerToFitnessClassEnrolment);
        }
        
        /// <summary>
        /// Delete customer to fitness class enrolment
        /// </summary>
        /// <param name="id">CustomerId</param>
        /// <param name="enrolmentId">EnrolmentId</param>
        /// <returns>Empty content</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}/enrolments/{enrolmentId}")]
        public async Task<IActionResult> DeleteCustomerToFitnessClassEnrolmentAsync(Guid id, Guid enrolmentId)
        {
            var customerToFitnessClassEnrolment = await _customerService.DeleteCustomerToFitnessClassEnrolmentAsync(id, enrolmentId);
            return HandleResponse(customerToFitnessClassEnrolment);
        }
        
        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="dto">UpdateCustomer.Command</param>
        /// <param name="id">CustomerId</param>
        /// <returns>Empty content</returns>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] UpdateCustomerDto dto)
        {
            var customerUpdated = await _customerService.UpdateAsync(id, dto);
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