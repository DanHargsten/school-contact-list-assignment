namespace Business.Models;

/// <summary>
/// Represents a contact with personal and address information.
/// </summary>
public class ContactModel
{
    public string Id { get; init; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;



    /// <summary>
    /// Returns a compact representation of the contact.
    /// </summary>
    /// <returns>A string with the contac'ts first and last name.</returns>
    public string CompactContact()
    {
        return $"{FirstName} {LastName}"; 
    }


    /// <summary>
    /// Returns a detailed representation of the contact.
    /// </summary>
    /// <returns>A detailed multi-line string with the contact's full information.</returns>
    public string DetailedContact()
    {
        return $@"
 Name:         {FirstName} {LastName}
 Email:        {Email}
 Phone:        {Phone}
 Address:      {Address}
               {PostalCode}, {City}
  
 ID:           {Id}";
    }
}