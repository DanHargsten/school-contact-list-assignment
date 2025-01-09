using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

/// <summary>
/// Defines the contract for a service managing contacts.
/// </summary>
public interface IContactService
{

    /// <summary>
    /// Adds a new contact based on the provided registration form.
    /// </summary>
    /// <param name="form">The registration form containing contact details.</param>
    void AddContact(ContactRegistrationForm form);


    /// <summary>
    /// Retrieves all contacts.
    /// </summary>
    /// <returns>A list of all contacts.</returns>
    List<ContactModel> GetAllContacts();


    /// <summary>
    /// Updates an existing contact based on the specified index.
    /// </summary>
    /// <param name="index">The index of the contact to update.</param>
    /// <param name="form">The updated contact details.</param>
    /// <returns>True if the update was successful, false otherwise.</returns>
    bool UpdateContact(int index, ContactRegistrationForm form);


    /// <summary>
    /// Deletes a contact based on the specified index.
    /// </summary>
    /// <param name="index">The index of the contact to delete.</param>
    /// <returns>True if the contact was successfully deleted, false otherwise.</returns>
    bool DeleteContact(int index);

    void SaveContacts();
}