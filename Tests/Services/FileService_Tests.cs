using Business.Models;
using Business.Services;
using System.Text.Json;


namespace Tests.Services;

public class FileService_Tests
{
    private const string TestDirectory = "TestData";
    private const string TestFile = "testfile.json";

    [Fact]
    public void SaveContentToFile_ShouldCreateFile()
    {
        // Arrange
        var fileService = new FileService(TestDirectory, TestFile);
        var contacts = new List<ContactModel>
        {
            new ContactModel
            {
                Id = "1",
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Phone = "1234567890",
                Address = "123 Street",
                PostalCode = "12345",
                City = "Testville"
            }
        };

        // Act
        fileService.SaveContentToFile(contacts);

        // Assert
        var filePath = Path.Combine(TestDirectory, TestFile);
        Assert.True(File.Exists(filePath));

        // Cleanup
        CleanupTestFiles();
    }



    [Fact]
    public void GetContactFromFile_ShouldReturnCorrectData()
    {
        // Arrange
        var fileService = new FileService(TestDirectory, TestFile);
        var expectedContacts = new List<ContactModel>
        {
            new ContactModel
            {
                Id = "1",
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Phone = "1234567890",
                Address = "123 Street",
                PostalCode = "12345",
                City = "Testville"
            }
        };

        var json = JsonSerializer.Serialize(expectedContacts);
        Directory.CreateDirectory(TestDirectory);
        File.WriteAllText(Path.Combine(TestDirectory, TestFile), json);

        // Act
        var result = fileService.GetContentFromFile();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result!);
        Assert.Equal(expectedContacts[0].FirstName, result[0].FirstName);

        // Cleanup
        CleanupTestFiles();
    }




    private void CleanupTestFiles()
    {
        if (Directory.Exists(TestDirectory))
        {
            Directory.Delete(TestDirectory, true);
        }
    }
}