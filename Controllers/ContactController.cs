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
        public async Task<Contact> CreateContact([FromBody] Contact contact)
        {
            // אם הגיע Id זמני/שלילי – מתעלמים כדי שה־DB יקצה Id אמיתי
            if (contact.Id <= 0) contact.Id = 0;

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



       [HttpPut, Route("syncFromClient")]
public async Task<ActionResult<IEnumerable<Contact>>> SyncFromClient([FromBody] Contact[] Clientcontacts)
{
    Contact[] ServerContacts = await _contactBL.GetAllContacts();
    int maxId = ServerContacts.Any() ? ServerContacts.Max(c => c.Id) : 0;

    foreach (Contact Contact in Clientcontacts)
    {
        if (ServerContacts.Any(item => item.Id == Contact.Id))
        {
            await _contactBL.EditContact(Contact);
        }
        else
        {
            maxId++;
            Contact.Id = maxId;
            await _contactBL.CreateContact(Contact);
        }
    }

    foreach (Contact Contact in ServerContacts)
    {
        if (!Clientcontacts.Any(item => item.Id == Contact.Id))
        {
            await _contactBL.DeleteContact(Contact.Id);
        }
    }

    // שינוי יחיד: מחזיר את הרשימה כפי שהיא בשרת אחרי הסנכרון
    var updatedList = await _contactBL.GetAllContacts();
    return Ok(updatedList);
}
    }
}
