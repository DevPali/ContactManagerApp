using ContactManagerApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerApp.Api.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDBContext _context;

        public ContactRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddContact(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public void UpdateContact(Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public Task<Contact> GetContactById(int id)
        {
            throw new NotImplementedException();
        }        
    }
}
