using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Address;
using FitnessManager.Domain.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class AddressesController : BaseController
    {
        /// <summary>
        /// Get list of addresses
        /// </summary>
        /// <returns>List of AddressDto</returns>
        [ProducesResponseType(typeof(IEnumerable<AddressDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await Mediator.Send(new GetAll.Query());
            return HandleResponse<IEnumerable<Address>, IEnumerable<AddressDto>>(result);
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
            var result = await Mediator.Send(new Get.Query { Id = id });
            return HandleResponse<Address, AddressDto>(result);
        }
    }
}