using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp.Dialogs;

public class menuDialog
{
    private readonly ContactService _contactService = new ContactService();

    public void ShowMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("======= CONTACT LIST =======");
            Console.WriteLine("1. View All Contacts");
            Console.WriteLine("2. Add New Contact");
            Console.WriteLine("3. Update Existing Contact");
            Console.WriteLine("4. Delete Contact");
            Console.WriteLine("Q. Quit Application");
            Console.Write("Choose an option: ");

            string option = Console.ReadLine()!;

            if (!string.IsNullOrEmpty(option))
            {
                switch (option.ToUpper())
                {
                    case "1":
                        ShowAllContacts(_contactService);
                        break;

                    case "2":
                        AddContact(_contactService);
                        break;

                    case "3":
                        Console.WriteLine("update");
                        break;

                    case "4":
                        Console.WriteLine("delete");
                        break;

                    case "Q":
                        Console.WriteLine("exit");
                        break;

                    default:
                        Console.WriteLine("fel?");
                        break;
                }
            }

            Console.ReadKey();
        }
    }



    // Reducera upprepning av kod genom att skapa en hjälpmetod för användarens input.
    private string GetInput(string promt)
    {
        Console.Write(promt);
        string? input = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(promt))
        {
            Console.WriteLine("Input can't be empty. Please try again.");
            Console.Write(promt);
            input = Console.ReadLine()!;
        }
        return input!;
    }



    // Väldigt simpel validering av email.
    private string GetValidatedEmail()
    {
        string email = GetInput("Email: ");
        while (!email.Contains("@") || !email.Contains("."))
        {
            Console.WriteLine("Invalid email format. Please try again");
            email = GetInput("Email: ");
        }
        return email;
    }



    // Visar initialt en kompakt lista som enbart inehåller för- och efternamn, samt ID.
    // Användaren kan sen välja en kontakt via ett index för att se fullständing information.
    public void ShowAllContacts(ContactService contactService)
    {
        while (true)
        {
            Console.Clear();

            var contacts = _contactService.GetAllContacts();
            if (contacts.Count != 0)
            {
                // Skriver ut en kompakt lista
                for (int i = 0; i < contacts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {contacts[i].CompactContact()}");
                }

                Console.WriteLine("\nEnter the number of a contact to view details, or press Enter to return.");
                string input = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(input))
                {
                    OutputMessage($"Invalid input. Please enter a number between 1 and {contacts.Count}.\nPress any key to return to the contact list.");
                    Console.ReadKey();
                    return;
                }

                // Skriver ut detaljerad lista
                if (int.TryParse(input, out int index) && index > 0 && index <= contacts.Count)
                {
                    Console.Clear();
                    Console.WriteLine(contacts[index - 1].DetailedContact());
                    Console.WriteLine("\nPress any key to return to the contact list.");
                    Console.ReadKey();
                }

                else
                {
                    OutputMessage($"Invalid input. Please enter a number between 1 and {contacts.Count}.\n\nPress any key to return to the contact list.");
                    Console.ReadKey();
                }
            }

            else
            {
                OutputMessage("No existing contacts");
                return;
            }
        }
    }
    

    // Lägg till kontakt
    public void AddContact(ContactService contactService)
    {
        Console.Clear();
        Console.WriteLine("Please enter contact information.");

        string firstName = GetInput("First Name: ");
        string lastName = GetInput("Last Name: ");
        string email = GetValidatedEmail();
        string phone = GetInput("Phone number: ");
        string address = GetInput("Address: ");
        string postalCode = GetInput("Postal Code: ");
        string city = GetInput("City: ");

        
        var newContact = new ContactModel
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Address = address,
            PostalCode = postalCode,
            City = city
        };

        contactService.AddContact(newContact);
        Console.Clear();
        Console.WriteLine($"Contact added successfully!\n{newContact.CompactContact()}");
        Console.WriteLine("\nPress any key to continue.");
    }



    public void OutputMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }
}