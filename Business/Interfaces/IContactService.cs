using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    // Create
    void AddContact(ContactModel contact);

    // Read
    List<ContactModel> GetAllContacts();

    // Update
    bool UpdateContact(int index, ContactModel updatedContact);

    // Delete
    bool DeleteContact(int index);
}