using Business.Dtos;
using Business.Factories;

namespace Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnEmptyContactRegistrationForm()
    {
        // Act
        var form = ContactFactory.Create();

        // Assert
        Assert.NotNull(form);
        Assert.IsType<ContactRegistrationForm>(form);
        Assert.Null(form.FirstName);
        Assert.Null(form.LastName);
    }


    [Fact]
    public void Create_ShouldConvertFormToContactModel()
    {
        // Arrange
        var form = new ContactRegistrationForm
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@domain.com",
            Phone = "1234567890",
            Address = "123 Street",
            PostalCode = "12345",
            City = "Doe-City"
        };

        // Act
        var contact = ContactFactory.Create(form);

        // Assert
        Assert.Equal(form.FirstName, contact.FirstName);        
        Assert.Equal(form.LastName, contact.LastName);
        Assert.Equal(form.Email, contact.Email);
        Assert.Equal(form.Phone, contact.Phone);
        Assert.Equal(form.Address, contact.Address);
        Assert.Equal(form.PostalCode, contact.PostalCode);
        Assert.Equal(form.City, contact.City);
    }
}