using ContactManager.Controllers;
using ContactManager.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.BL
{
    public class ContactBL
    {
        private readonly AppDbContext _context;

        public ContactBL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact[]> GetAllContacts()
        {
            return await _context.Contacts.OrderBy(c => c.Name).ToArrayAsync();
        }
        public async Task<Contact> CreateContact(Contact contact)
        {
             _context.Contacts.Add(contact);
            _context.SaveChanges();
            return contact;
        }
        public async Task<Contact> DeleteContact(int id)
        {
            Contact c = _context.Contacts.FirstOrDefault(co => co.Id == id);
            _context.Contacts.Remove(c);
            _context.SaveChanges();
            return c;
        }
        public async Task<Contact> EditContact(Contact contact)
        {
            Contact c = _context.Contacts.FirstOrDefault(co => co.Id == contact.Id);
            DeleteContact(c.Id);
            CreateContact(contact);
            _context.SaveChanges();
            return contact;
        }
    }
}
