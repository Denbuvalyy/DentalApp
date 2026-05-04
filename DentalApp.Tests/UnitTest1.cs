namespace DentalApp.Tests;

public class SampleTests
{
    [Fact]
    public void Should_Add_Numbers()
    {
        var result = 2 + 2;
        Assert.Equal(4, result);
        //Assert.Equal(4, result);
    }
    
    [Fact]
    public void Should_Add_FreshNumbers()
    {
        var result = 2 + 3;
        Assert.Equal(5, result);
        //Assert.Equal(4, result);
    }
}