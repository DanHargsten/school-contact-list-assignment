using Business.Helpers;

namespace Tests.Helpers;

public class IdGenerator_Tests
{
    [Fact]
    public void Generate_ShouldReturnUniqueId()
    {
        // Act
        var id1 = IdGenerator.Generate();
        var id2 = IdGenerator.Generate();

        // Assert
        Assert.NotEqual(id1, id2);
        Assert.False(string.IsNullOrEmpty(id1));
        Assert.False(string.IsNullOrEmpty(id2));
    }
}
