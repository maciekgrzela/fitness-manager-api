using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Contact.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Contact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.API.Controllers
{
    public class ContactsController : BaseController
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        
        /// <summary>
        /// Get list of contacts
        /// </summary>
        /// <returns>List of ContactDto</returns>
        [ProducesResponseType(typeof(IEnumerable<ContactDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var contacts = await _contactService.GetAllAsync();
            return OkDto<IEnumerable<ContactEntity>, IEnumerable<ContactDto>>(contacts);
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
            var contact = await _contactService.GetByIdAsync(id);
            return HandleResponse<ContactEntity, ContactDto>(contact);
        }
    }
}