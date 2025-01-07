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
    /// Creates an empty instance of <see cref="ContactRegistrationForm"/>.
    /// </summary>
    public static ContactRegistrationForm Create() => new();


    /// <summary>
    /// Creates a <see cref="ContactModel"/> from a given <see cref="ContactRegistrationForm"/>.
    /// </summary>
    /// <param name="form">The input form containing contact details.</param>
    /// <returns>A new instance of <see cref="ContactModel"/>.</returns>
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