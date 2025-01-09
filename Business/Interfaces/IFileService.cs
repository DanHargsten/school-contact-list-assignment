using Business.Models;

namespace Business.Interfaces;

public interface IFileService
{
    List<ContactModel>? GetContentFromFile();
    void SaveContentToFile(List<ContactModel> contacts);
}