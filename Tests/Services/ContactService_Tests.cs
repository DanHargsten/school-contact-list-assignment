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
    public void AddContact_ShouldAddContactToListAndSaveToFile()
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
}
