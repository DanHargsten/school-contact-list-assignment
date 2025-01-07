namespace Business.Models;

/// <summary>
/// Represents a contact with detailed information.
/// </summary>
public class ContactModel
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;



    /// <summary>
    /// Returns a compact representation of the contact, containing only the first and last name.
    /// </summary>
    public string CompactContact()
    {
        return $"{FirstName} {LastName}"; 
    }


    /// <summary>
    /// Returns a detailed representation of the contact with all fields.
    /// </summary>
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