using Business.Models;
using Business.Services;
using System;

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
                        ShowAllContacts();
                        break;

                    case "2":
                        AddContact();
                        break;

                    case "3":
                        UpdateContact();
                        break;

                    case "4":
                        DeleteContact();
                        break;

                    case "Q":
                        ExitApplication();
                        break;

                    default:
                        Console.WriteLine("fel?");
                        break;
                }
            }
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
    public void ShowAllContacts()
    {
        string? errorMessage = null;

        while (true)
        {
            Console.Clear();

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine();
            }

            var contacts = _contactService.GetAllContacts();
            if (contacts.Count != 0)
            {
                // Skriver ut en kompakt lista
                for (int i = 0; i < contacts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {contacts[i].CompactContact()}");
                }

                Console.Write("\nEnter the number of a contact to view details, or press Enter to return: ");
                string input = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(input))
                {
                    return;
                }

                // Skriver ut detaljerad lista
                if (int.TryParse(input, out int index) && index > 0 && index <= contacts.Count)
                {
                    Console.Clear();
                    Console.WriteLine(contacts[index - 1].DetailedContact());
                    Console.WriteLine("\nPress any key to return to the contact list.");
                    Console.ReadKey();
                    errorMessage = null;
                }
                else
                {
                    errorMessage = $"Invalid input. Please enter a number between 1 and {contacts.Count}.";
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
    public void AddContact()
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

        _contactService.AddContact(newContact);
        Console.Clear();
        Console.WriteLine($"Contact added successfully!\n{newContact.CompactContact()}");
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
    }



    // Uppdatera existerande kontakt
    public void UpdateContact()
    {
        var contacts = _contactService.GetAllContacts();
        if (contacts.Count == 0)
        {
            OutputMessage("No existing contacts to update.");
            return;
        }

        Console.Clear();
        for (int i = 0; i < contacts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {contacts[i].CompactContact()}");
        }

        Console.Write("\nEnter the number of the contact you want to update, or press Enter to return: ");
        string input = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int index) || index < 1 || index > contacts.Count)
        {
            OutputMessage("Invalid input. Returning to the menu.");
            return;
        }

        var contactToUpdate = contacts[index - 1];
        Console.Clear();
        Console.WriteLine($"Updating contact: {contactToUpdate.CompactContact()}");
        Console.WriteLine("1. First Name");
        Console.WriteLine("2. Last Name");
        Console.WriteLine("3. Email");
        Console.WriteLine("4. Phone");
        Console.WriteLine("5. Address");
        Console.WriteLine("6. Postal Code");
        Console.WriteLine("7. City");


        string fieldChoice = Console.ReadLine()!;
        switch (fieldChoice)
        {
            case "1":
                contactToUpdate.FirstName = GetInput("Enter new First Name: ");
                break;
            
            case "2":
                contactToUpdate.LastName = GetInput("Enter new Last Name: ");
                break;

            case "3":
                contactToUpdate.Email = GetValidatedEmail();
                break;

            case "4":
                contactToUpdate.Phone = GetInput("Enter new Phone Number:");
                break;

            case "5":
                contactToUpdate.Address = GetInput("Enter new Address: ");
                break;

            case "6":
                contactToUpdate.PostalCode = GetInput("Enter new Postal Code: ");
                break;

            case "7":
                contactToUpdate.City = GetInput("Enter new City: ");
                break;

            default:
                OutputMessage("Invalid field choise. Returning to the menu.");
                return;
        }

        _contactService.SaveContacts();
        OutputMessage("Contact updated successfully.");

    }



    public void DeleteContact()
    {
        var contacts = _contactService.GetAllContacts();
        if (contacts.Count == 0)
        {
            OutputMessage("No existing contacts to delete.");
        }

        Console.Clear();
        for (int i = 0; i < contacts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {contacts[i].CompactContact()}");
        }

        Console.Write("\nEnter the number of the contact you want to delete, or press Enter to return: ");
        string input = Console.ReadLine()!;

        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int index) || index > 1 || index > contacts.Count)
        {
            OutputMessage("Invalid input. Returning to the menu.");
            return;
        }

        var contactToDelete = contacts[index - 1];
        Console.WriteLine($"\nAre you sure you want to delete this contact?");
        Console.WriteLine($"{contactToDelete.CompactContact()}");
        Console.Write($"\nEnter Y to confirm, or any other key to cancel: ");
        string confirmation = Console.ReadLine()!;

        if (confirmation.ToUpper() == "Y")
        {
            _contactService.DeleteContact(index - 1);
            _contactService.SaveContacts();
            OutputMessage("Contact deleted successfully.");
        }
        else
        {
            OutputMessage("Deletion canceled.");
        }
    }





    public void ExitApplication()
    {
        Console.WriteLine("Are you sure you want to exit the application?");
        Console.Write("\nEnter Y to confirm, or any other key to cancel: ");
        string confirmation = Console.ReadLine()!;

        if (confirmation.ToUpper() == "Y")
        {
            Environment.Exit(0);
        }
        else
        {
            return;
        }
    }






    public void OutputMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }
}