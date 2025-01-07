using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

/// <summary>
/// A service for managing contacts, including CRUD operations.
/// </summary>
public class ContactService : IContactService
{
    private readonly List<ContactModel> contacts = new();
    private readonly FileService fileService;


    /// <summary>
    /// Initializes a new instance of <see cref="ContactService"/>.
    /// Loads existing contacts from file if available.
    /// </summary>
    public ContactService()
    {
        fileService = new FileService();
        contacts = fileService.GetContentFromFile() ?? new List<ContactModel>();
    }



    public void AddContact(ContactRegistrationForm form)
    {
        var contact = ContactFactory.Create(form);
        contacts.Add(contact);
        SaveContacts();
    }



    public List<ContactModel> GetAllContacts() => contacts;
    


    public bool UpdateContact(int index, ContactRegistrationForm form)
    {
        if (index >= 0 && index < contacts.Count)
        {
            var updatedContact = ContactFactory.Create(form);
            contacts[index] = updatedContact;
            SaveContacts();
            return true;
        }
        return false;
    }



    public bool DeleteContact(int index)
    {
        if (index >= 0 && index <= contacts.Count)
        {
            contacts.RemoveAt(index);
            SaveContacts();
            return true;
        }
        return false;
    }



    public void SaveContacts() => fileService.SaveContentToFile(contacts);
}