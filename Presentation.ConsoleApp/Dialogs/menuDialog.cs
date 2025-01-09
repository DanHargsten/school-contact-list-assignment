using Business.Dtos;
using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialog
{
    //private readonly ContactService _contactService = new ContactService(new FileService());
    private readonly IContactService _contactService;

    public MenuDialog(IContactService contactService)
    {
        _contactService = contactService;
    }

    /// <summary>
    /// Displays the main menu and handles user input for navigation.
    /// </summary>
    public void ShowMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("======= CONTACT LIST =======\n");
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
                        Console.WriteLine("Invalid option. Please select a valid option from the menu.");
                        break;
                }
            }
        }
    }



    /// <summary>
    /// Prompts the user for input with a custom message and validates that it is not empty.
    /// </summary>
    /// <param name="prompt">The message displayed to the user.</param>
    /// <returns>A validated, non-empty string.</returns>
    private string GetInput(string promt)
    {
        Console.Write(promt);
        string? input = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input can't be empty. Please try again.");
            Console.Write(promt);
            input = Console.ReadLine()!;
        }
        return input!;
    }



    /// <summary>
    /// Prompts the user for an email and validates that it is in a proper format.
    /// </summary>
    /// <returns>A validated email string.</returns>
    private string GetValidatedEmail(string prompt)
    {
        Console.Write(prompt);
        string email = Console.ReadLine()!;
        while (!EmailHelper.IsValidEmail(email))
        {
            Console.WriteLine("Invalid email format. Please try again.");
            Console.Write(prompt);
            email = Console.ReadLine()!;
        }

        return email;
    }



    /// <summary>
    /// Displays a list of all contacts in compact format and allows the user to view details for a specific contact.
    /// </summary>
    public void ShowAllContacts()
    {
        string? errorMessage = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("======= VIEW ALL CONTACTS =======\n");


            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine();
            }

            var contacts = _contactService.GetAllContacts();
            if (contacts.Count != 0)
            {
                // Visa alla kontakter i en kompakt lista
                for (int i = 0; i < contacts.Count; i++)
                {
                    Console.WriteLine($" {i + 1}. {contacts[i].CompactContact()}");
                }

                Console.WriteLine("\n 0. Return to main menu");
                Console.Write("\nEnter the number of a contact to view details, or 0 to return to main menu: ");
                string input = Console.ReadLine()!;

                if (input == "0") return;

                if (string.IsNullOrWhiteSpace(input)) return;
                

                // Skriver ut detaljerad information för vald kontakt
                if (int.TryParse(input, out int index) && index > 0 && index <= contacts.Count)
                {
                    Console.Clear();
                    Console.WriteLine("======= CONTACT DETAILS =======\n");
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
                OutputMessage("No existing contacts. Press any key to return to the main menu.");
                Console.ReadKey();
                return;
            }         
        }
    }




    /// <summary>
    /// Prompts the user to enter details for a new contact and adds it to the contact list.
    /// </summary>
    public void AddContact()
    {
        Console.Clear();
        Console.WriteLine("======= ADD CONTACT =======\n");
        Console.WriteLine("Please enter contact information.");

        var contactForm = new ContactRegistrationForm
        {
            FirstName = GetInput("First Name: "),
            LastName = GetInput("Last Name: "),
            Email = GetValidatedEmail("Email: "),
            Phone = GetInput("Phone number: "),
            Address = GetInput("Address: "),
            PostalCode = GetInput("Postal Code: "),
            City = GetInput("City: ")
        };


        var newContact = ContactFactory.Create(contactForm);
        _contactService.AddContact(contactForm);

        Console.Clear();
        Console.WriteLine($"{newContact.CompactContact()} added successfully!");
        Console.WriteLine("\nPress any key to return to the main menu.");
        Console.ReadKey();
    }



    /// <summary>
    /// Allows the user to update details for an existing contact.
    /// </summary>
    public void UpdateContact()
    {
        var contacts = _contactService.GetAllContacts();
        if (contacts.Count == 0)
        {
            OutputMessage("No existing contacts to update. Press any key to return to the main menu.");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine("======= UPDATE CONTACTS =======\n");
        for (int i = 0; i < contacts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {contacts[i].CompactContact()}");
        }

        Console.Write("\nEnter the number of the contact you want to update, or 0 to cancel: ");
        string input = Console.ReadLine()!;

        if (input == "0") return;

        if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int index) || index < 1 || index > contacts.Count)
        {
            OutputMessage("Invalid input. Returning to the menu.");
            return;
        }

        var contactToUpdate = contacts[index - 1];
        var maskedEmail = EmailHelper.MaskEmail(contactToUpdate.Email);

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=========================================");
            Console.WriteLine($" Updating contact: {contactToUpdate.CompactContact()}");
            Console.WriteLine($"=========================================");
            Console.WriteLine($" Choose the field to update:");
            Console.WriteLine($" 1. First Name  ({contactToUpdate.FirstName})");
            Console.WriteLine($" 2. Last Name   ({contactToUpdate.LastName})");
            Console.WriteLine($" 3. Email       ({maskedEmail})");
            Console.WriteLine($" 4. Phone       ({contactToUpdate.Phone})");
            Console.WriteLine($" 5. Address     ({contactToUpdate.Address})");
            Console.WriteLine($" 6. Postal Code ({contactToUpdate.PostalCode})");
            Console.WriteLine($" 7. City        ({contactToUpdate.City})");
            Console.WriteLine();
            Console.WriteLine($" 0. Cancel");


            Console.Write("\nEnter the number of the field you want to update: ");
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
                    contactToUpdate.Email = GetValidatedEmail("Enter new Email: ");
                    break;

                case "4":
                    contactToUpdate.Phone = GetInput("Enter new Phone Number: ");
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

                case "0":
                    OutputMessage("Update canceled. Press any key to return to the main menu.");
                    Console.ReadKey();
                    return;

                default:
                    OutputMessage("Invalid field choice. Please try again.");
                    Console.ReadKey();
                    continue;
            }

            _contactService.SaveContacts();
            OutputMessage("Contact updated successfully.\nPress any key to return to the main menu.");
            Console.ReadKey();
            return;
        }


    }



    /// <summary>
    /// Deletes a selected contact from the list after confirmation from the user.
    /// </summary>
    public void DeleteContact()
    {
        var contacts = _contactService.GetAllContacts();
        if (contacts.Count == 0)
        {
            OutputMessage("No existing contacts to delete.\nPress any key to return to the main menu.");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine("======= DELETE CONTACT =======\n");
        for (int i = 0; i < contacts.Count; i++)
        {
            Console.WriteLine($" {i + 1}. {contacts[i].CompactContact()}");
        }

        Console.WriteLine("\n 0. Return to main menu");
        Console.Write("\nEnter the number of the contact you want to delete, or 0 to cancel: ");
        string input = Console.ReadLine()!;

        if (input == "0") return;

        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int index) || index < 1 || index > contacts.Count)
        {
            OutputMessage("Invalid input. Returning to the menu.");
            return;
        }

        var contactToDelete = contacts[index - 1];

        Console.Clear();
        Console.WriteLine($"Are you sure you want to delete this contact?");
        Console.WriteLine($"{contactToDelete.CompactContact()}");
        Console.Write($"\nEnter Y to confirm, or any other key to cancel: ");
        string confirmation = Console.ReadLine()!;

        if (confirmation.ToUpper() == "Y")
        {
            _contactService.DeleteContact(index - 1);
            _contactService.SaveContacts();
            OutputMessage("Contact deleted successfully.\nPress any key to return to the main menu.");
        }
        else
        {
            OutputMessage("Deletion canceled.\nPress any key to return to the main menu.");
        }
    }





    public void ExitApplication()
    {
        Console.Clear();
        Console.WriteLine("\nExiting application. Goodbye!");
        Environment.Exit(0);
    }





    public void OutputMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }
}