# Mockery
Helper methods to use as shortcuts when creating mocks with Moq.

Example usage:
```
public class TestCases
{
    IDependency dep;
    ITestable instance() => new Testable( dep );

    readonly Mock<IDependency> mockDep;

    public TestCases()
    {
        // this would create a strict mock
        mockDep = Mockery.Of( out dep );

        // this would create a loose mock
        // mockDep = Mockery.Of( MockBehavior.Loose, out dep );
    }

    public class Constructor : TestCases
    {
        [Fact]
        public void requires_dep()
        {
            arg = null;
            Assert.Throws<ArgumentNullException>( nameof(dep), instance );
        }
    }

    public class DoWork : TestCases
    {
        Guid arg = Guid.NewGuid();
        IResult method() => instance().DoWork( arg );
                
        public void returns_result_from_dependency()
        {
            // here is how to mock an instance without setup
            var expected = Mockery.Of<IResult>();

            mockDep.Setup( _=> _.GetResult( arg ) ).Returns( expected );

            var actual = method();
            Assert.Same( expected, actual );
        }
    }
}
```
