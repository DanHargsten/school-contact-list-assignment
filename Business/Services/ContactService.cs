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
    private readonly IFileService _fileService;
    private readonly List<ContactModel> _contacts;


    /// <summary>
    /// Initializes a new instance of <see cref="ContactService"/>.
    /// Loads existing contacts from file if available.
    /// </summary>
    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
        _contacts = _fileService.GetContentFromFile() ?? new List<ContactModel>();
    }



    public void AddContact(ContactRegistrationForm form)
    {
        var contact = ContactFactory.Create(form);
        _contacts.Add(contact);
        SaveContacts();
    }



    public List<ContactModel> GetAllContacts() => _contacts;
    


    public bool UpdateContact(int index, ContactRegistrationForm form)
    {
        if (index >= 0 && index < _contacts.Count)
        {
            var updatedContact = ContactFactory.Create(form);
            _contacts[index] = updatedContact;
            SaveContacts();
            return true;
        }
        return false;
    }



    public bool DeleteContact(int index)
    {
        if (index >= 0 && index <= _contacts.Count)
        {
            _contacts.RemoveAt(index);
            SaveContacts();
            return true;
        }
        return false;
    }



    public void SaveContacts() => _fileService.SaveContentToFile(_contacts);
}