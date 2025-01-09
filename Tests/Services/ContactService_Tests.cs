using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly ContactService _contactService;

    public ContactService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _contactService = new ContactService(_fileServiceMock.Object);
    }

    [Fact]
    public void AddContact_WhenContactIsValid_ShouldAddContactToList()
    {
        // Arrange
        var contactForm = new ContactRegistrationForm
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Phone = "1234567890",
            Address = "123 Street",
            PostalCode = "12345",
            City = "Testville"
        };

        _fileServiceMock.Setup(x => x.SaveContentToFile(It.IsAny<List<ContactModel>>()));

        // Act
        _contactService.AddContact(contactForm);

        // Assert
        _fileServiceMock.Verify(x => x.SaveContentToFile(It.IsAny<List<ContactModel>>()), Times.Once);
    }


    [Fact]
    public void DeleteContact_WhenIndexIsValid_ShouldRemoveContactFromList()
    {
        // Arrange
        var fileServiceMock = new Mock<IFileService>();
        var contactService = new ContactService(_fileServiceMock.Object);
        var contact = new ContactModel
        {
            Id = "12345",
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com"
        };

        contactService.AddContact(new ContactRegistrationForm
        {
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
        });

        // Act
        var result = contactService.DeleteContact(0);

        // Assert
        Assert.True(result);
        Assert.Empty(contactService.GetAllContacts());
    }


    [Fact]
    public void DeleteContact_WhenIndexIsInvalid_ShouldNotRemoveAnyContacts()
    {
        // Arrange
        var fileServiceMock = new Mock<IFileService>();
        var contactService = new ContactService(fileServiceMock.Object);
        var contact = new ContactModel
        {
            Id = "12345",
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com"
        };

        contactService.AddContact(new ContactRegistrationForm
        {
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email
        });

        // Act
        var result = contactService.DeleteContact(1);

        // Assert
        Assert.False(result);
        Assert.Single(contactService.GetAllContacts());
    }


    [Fact]
    public void UpdateContact_WhenIndexIsValid_ShouldUpdateContactDetails()
    {
        // Arrange
        var fileServiceMock = new Mock<IFileService>();
        var contactService = new ContactService(fileServiceMock.Object);
        contactService.AddContact(new ContactRegistrationForm
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com"
        });

        var updatedForm = new ContactRegistrationForm
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "janedoe@example.com"
        };

        // Act
        var result = contactService.UpdateContact(0, updatedForm);

        // Assert
        Assert.True(result);
        var updatedContact = contactService.GetAllContacts()[0];
        Assert.Equal(updatedForm.FirstName, updatedContact.FirstName);
        Assert.Equal(updatedForm.LastName, updatedContact.LastName);
        Assert.Equal(updatedForm.Email, updatedContact.Email);
    }

    [Fact]
    public void UpdateContact_WhenIndexIsInvalid_ShouldNotUpdateAnyContact()
    {
        // Arrange
        var fileServiceMock = new Mock<IFileService>();
        var contactService = new ContactService(fileServiceMock.Object);
        contactService.AddContact(new ContactRegistrationForm
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com"
        });

        var updatedForm = new ContactRegistrationForm
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "janedoe@example.com"
        };

        // Act
        var result = contactService.UpdateContact(1, updatedForm);

        // Assert
        Assert.False(result);
        var contact = contactService.GetAllContacts()[0];
        Assert.Equal("John", contact.FirstName);
        Assert.Equal("Doe", contact.LastName);
        Assert.Equal("johndoe@example.com", contact.Email);
    }

}
