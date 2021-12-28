using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Address;
using FitnessManager.BusinessLogic.Address.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class AddressesController : BaseController
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        
        /// <summary>
        /// Get list of addresses
        /// </summary>
        /// <returns>List of AddressDto</returns>
        [ProducesResponseType(typeof(IEnumerable<AddressDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var addresses = await _addressService.GetAllAsync();
            return OkDto<IEnumerable<AddressEntity>, IEnumerable<AddressDto>>(addresses);
        }
        
        /// <summary>
        /// Get address for given id
        /// </summary>
        /// <returns>AddressDto</returns>
        [ProducesResponseType(typeof(IEnumerable<AddressDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var address = await _addressService.GetByIdAsync(id);
            return HandleResponse<AddressEntity, AddressDto>(address);
        }
    }
}