using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly List<ContactModel> contacts = new();
    private readonly FileService fileService;

    public ContactService()
    {
        fileService = new FileService();
        contacts = fileService.GetContentFromFile() ?? new List<ContactModel>();
    }



    public void AddContact(ContactModel contact)
    {
        contacts.Add(contact);
        SaveContacts();
    }


    public List<ContactModel> GetAllContacts()
    {
        return contacts;
    }


    public bool UpdateContact(int index, ContactModel updatedContact)
    {
        if (index >= 0 && index < contacts.Count)
        {
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


    public void SaveContacts()
    {
        fileService.SaveContentToFile(contacts);
    }
}