using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

/// <summary>
/// Provides services to manage contacts, including adding, retrieving, updating, and deleting contacts.
/// </summary>
public class ContactService : IContactService
{
    private readonly IFileService _fileService;
    private readonly List<ContactModel> _contacts;


    /// <summary>
    /// Initializes a new instance of <see cref="ContactService"/>.
    /// </summary>
    /// <param name="fileService">The file service used to save and load contacts</param>
    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
        _contacts = _fileService.GetContentFromFile() ?? new List<ContactModel>();
    }



    /// <inheritdoc/>
    public void AddContact(ContactRegistrationForm form)
    {
        var contact = ContactFactory.Create(form);
        _contacts.Add(contact);
        SaveContacts();
    }



    /// <inheritdoc/>
    public List<ContactModel> GetAllContacts() => _contacts;



    /// <inheritdoc/>
    public bool UpdateContact(int index, ContactRegistrationForm form)
    {
        if (index >= 0 && index < _contacts.Count)
        {
            //var updatedContact = ContactFactory.Create(form);
            //_contacts[index] = updatedContact;
            //SaveContacts();
            //return true;

            _contacts[index].FirstName = form.FirstName;
            _contacts[index].LastName = form.LastName;
            _contacts[index].Email = form.Email;
            _contacts[index].Phone = form.Phone;
            _contacts[index].Address = form.Address;
            _contacts[index].PostalCode = form.PostalCode;
            _contacts[index].City = form.City;

            SaveContacts();
            return true;
        }
        return false;
    }



    /// <inheritdoc/>
    public bool DeleteContact(int index)
    {
        if (index >= 0 && index < _contacts.Count)
        {
            _contacts.RemoveAt(index);
            SaveContacts();
            return true;
        }
        return false;
    }



    /// <summary>
    /// Saves the list of contacts to a file.
    /// </summary>
    public void SaveContacts() => _fileService.SaveContentToFile(_contacts);
}