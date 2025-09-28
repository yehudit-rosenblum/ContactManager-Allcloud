using ContactManager.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ContactBL _contactBL;
        public ContactController(ILogger<ContactController> logger,ContactBL contactBL)
        {
            _logger = logger;
            _contactBL = contactBL;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            return  await _contactBL.GetAllContacts();
        }

        [HttpPost]
        public async Task<Contact> CreateContact(Contact contact)
        {
            return await _contactBL.CreateContact(contact);
        }
        [HttpDelete]
        public async Task<Contact> DeleteContact(int id)
        {
            return await _contactBL.DeleteContact(id);
        }



        [HttpPut]
        public async Task<Contact> EditContact(Contact contact)
        {
            return await _contactBL.EditContact(contact);
        }
    }
}
