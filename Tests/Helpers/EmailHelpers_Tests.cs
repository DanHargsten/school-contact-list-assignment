using Business.Helpers;

namespace Tests.Helpers;

public class EmailHelpers_Tests
{
    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("invalid-email", false)]
    [InlineData("test@example", false)]
    [InlineData("test.domain@", false)]
    [InlineData("test@.com", false)]
    [InlineData("test@domain.co.uk", true)]

    public void IsValidEmail_ShouldValidateEmailCorrectly(string email, bool expected)
    {
        // Act
        var result = EmailHelper.IsValidEmail(email);

        // Assert
        Assert.Equal(expected, result);
    }


    [Theory]
    [InlineData("john@example.com", "j****@example.com")]
    [InlineData("a@domain.com", "a****@domain.com")]
    [InlineData("ab@domain.com", "a****@domain.com")]
    [InlineData("invalid-email", "invalid-email")]
    public void MaskEmail_ShouldMaskCorrectly(string email, string expected)
    {
        // Act
        var result = EmailHelper.MaskEmail(email);

        // Assert
        Assert.Equal(expected, result);
    }
}
