using Business.Models;
using System.Text.Json;

namespace Business.Services;

public class FileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;

    public FileService(string directoryPath = "Data", string fileName = "contactlist.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(directoryPath, fileName);
    }


    // Save to file
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



    // Load data from file
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