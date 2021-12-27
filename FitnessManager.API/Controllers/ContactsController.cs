using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Contact;
using FitnessManager.Domain.Contact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class ContactsController : BaseController
    {
        /// <summary>
        /// Get list of contacts
        /// </summary>
        /// <returns>List of ContactDto</returns>
        [ProducesResponseType(typeof(IEnumerable<ContactDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await Mediator.Send(new GetAll.Query());
            return HandleResponse<IEnumerable<Contact>, IEnumerable<ContactDto>>(result);
        }
        
        /// <summary>
        /// Get contact for given id
        /// </summary>
        /// <returns>ContactDto</returns>
        [ProducesResponseType(typeof(IEnumerable<ContactDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid id)
        {
            var result = await Mediator.Send(new Get.Query { Id = id });
            return HandleResponse<Contact, ContactDto>(result);
        }
    }
}