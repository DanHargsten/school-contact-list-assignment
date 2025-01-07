namespace Business.Models;

public class ContactModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;


    public string CompactContact()
    {
        return $"{FirstName} {LastName}"; 
    }

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
