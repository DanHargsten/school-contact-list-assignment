namespace Business.Helpers;

/// <summary>
/// Provides utility methods for email validation and masking.
/// </summary>
public static class EmailHelper
{
    /// <summary>
    /// Checks if an email address is in a valid format.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns>True if the email address is valid, false otherwise.</returns>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        var atIndex = email.IndexOf('@');
        var dotIndex = email.LastIndexOf('.');

        // Kontrollera om "@" finns och inte är det första tecknet, och "." finns efter "@"
        return atIndex > 0 && dotIndex > atIndex + 1 && dotIndex < email.Length - 1;
    }






    // Importerad från ChatGPT
    // En enkel metod för att maskera e-postadresser när användaren uppdaterar en kontakt.
    // Nuvarande e-postadress visas i maskerat format för att ge en liten känsla av autenticitet, 
    // även om denna lilla applikation inte har några säkerhetskrav.
    //
    // Metoden visar endast den första bokstaven i e-postadressen och behåller hela domänen.
    // Exempel:
    //   test@domain.se --> t****@domain.se


    /// <summary>
    /// Masks an email address to hide part of it for display purposes.
    /// </summary>
    /// <param name="email">The email address to mask.</param>
    /// <returns>A masked email address.</returns>
    public static string MaskEmail(string email)
    {
        var atIndex = email.IndexOf('@');
        if (atIndex <= 0) return email;

        var visiblePart = email.Substring(0, 1);
        var domain = email.Substring(atIndex); 
        return $"{visiblePart}****{domain}";
    }
}