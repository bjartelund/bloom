using bloom;

namespace Test;

public class UnitTest1
{
    [Fact]
    public void Existing_Elements_Should_Be_Found()
    {
        var bloom = new Bloom(2,14);
        bloom.Insert("abc");
        bloom.Insert("def");
        var search = bloom.Search("def");
        search.Should().BeTrue();
    }

    [Fact]
    public void Non_Existing_Elements_Should_Return_False()
    {
        var bloom = new Bloom(2,14);
        bloom.Insert("abc");
        bloom.Insert("def");
        var search = bloom.Search("gfh");
        search.Should().BeFalse();
    }
}