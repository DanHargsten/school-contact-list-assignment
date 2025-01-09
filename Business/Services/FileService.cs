using Business.Interfaces;
using Business.Models;
using System.Text.Json;

namespace Business.Services;

/// <summary>
/// Provides functionality to save and load contact data to and from a file.
/// </summary>
public class FileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileService"/> class.
    /// </summary>
    /// <param name="directoryPath">The directory where the file is stored</param>
    /// <param name="fileName">The name of the file</param>
    public FileService(string directoryPath = "Data", string fileName = "contactlist.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(directoryPath, fileName);
    }



    /// <summary>
    /// Saves a list of contacts to a file in JSON format.
    /// </summary>
    /// <param name="contacts">The list of contacts to save.</param>
    public void SaveContentToFile(List<ContactModel> contacts)
    {
        try
        {
            if (contacts != null)
            {
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }

                var json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving contacts: {ex.Message}");
            File.AppendAllText("error-log.txt", $"{DateTime.Now}: {ex.Message}");
        }
    }



    /// <summary>
    /// Loads a list of contacts from a file in JSON format.
    /// </summary>
    /// <returns>A list of contacts, or null if the file does not exist or is invalid.</returns>
    public List<ContactModel>? GetContentFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<ContactModel>>(json);
            }
        }
        
        catch (JsonException ex)
        {
            Console.WriteLine($"Error readon contacts: Invalid JSON format. {ex.Message}");
            File.AppendAllText("error-log.txt", $"{DateTime.Now}: {ex.Message}");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error loading contacts: {ex.Message}");
            File.AppendAllText("error-log.txt", $"{DateTime.Now}: {ex.Message}");
        }

        return null;
    }
}