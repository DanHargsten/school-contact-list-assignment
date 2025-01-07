namespace Business.Dtos;

/// <summary>
/// A DTO for registering new contacts.
/// Holds the basic contact information provided by the user during creation or update.
/// </summary>
public class ContactRegistrationForm
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}