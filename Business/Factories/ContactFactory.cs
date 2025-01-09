using Business.Dtos;
using Business.Helpers;
using Business.Models;

namespace Business.Factories;

/// <summary>
/// A factory class for creating instances of <see cref="ContactModel"/> and <see cref="ContactRegistrationForm"/>.
/// </summary>
public class ContactFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="ContactRegistrationForm"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="ContactRegistrationForm"/></returns>
    public static ContactRegistrationForm Create() => new();


    /// <summary>
    /// Creates a new instance of <see cref="ContactModel"/> based on the provided registration form.
    /// </summary>
    /// <param name="form">The contact registration form containing user-provided details.</param>
    /// <returns>A new instance of <see cref="ContactModel"/> populated with data from the registration form.</returns>
    public static ContactModel Create(ContactRegistrationForm form) => new()
    {
        Id = IdGenerator.Generate(),
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email,
        Phone = form.Phone,
        Address = form.Address,
        PostalCode = form.PostalCode,
        City = form.City
    };
}