using ContactManagerApp.Models;

namespace ContactManagerApp.Api.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> GetContactById(int id);
        Task AddContact(Contact contact);
        void UpdateContact(Contact contact);
        Task DeleteContact(Contact contact);
    }
}
