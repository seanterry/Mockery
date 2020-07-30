using System;
using Xunit;

namespace Moq
{
    public class MockeryTests
    {
        public interface IMockable
        {
            void DoWork();
        }

        public class BehaviorCases : TheoryData<MockBehavior>
        {
            public BehaviorCases()
            {
                Add( MockBehavior.Strict );
                Add( MockBehavior.Loose );
            }
        }

        public class MockedInstanceTests
        {
            [Theory]
            [ClassData( typeof( BehaviorCases ) )]
            public void when_behavior_specified_returns_mocked_instance_with_given_behavior( MockBehavior behavior )
            {
                var actual = Mockery.Of<IMockable>( behavior );
                Assert.NotNull( actual );

                switch ( behavior )
                {
                    case MockBehavior.Loose:
                        actual.DoWork();
                        break;

                    case MockBehavior.Strict:
                        Assert.Throws<MockException>( actual.DoWork );
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            [Fact]
            public void when_behavior_unspecified_returns_mocked_instance_with_strict_behavior()
            {
                var actual = Mockery.Of<IMockable>();
                Assert.NotNull( actual );
                Assert.Throws<MockException>( actual.DoWork );
            }
        }

        public class MockTests
        {
            [Theory]
            [ClassData( typeof( BehaviorCases ) )]
            public void when_behavior_specified_returns_mock_with_given_behavior( MockBehavior behavior )
            {
                var actual = Mockery.Of( behavior, out IMockable mocked );

                Assert.IsType<Mock<IMockable>>( actual );
                Assert.Equal( behavior, actual.Behavior );
                Assert.Same( actual.Object, mocked );
            }

            [Fact]
            public void when_behavior_unspecified_returns_mock_with_strict_behavior()
            {
                var actual = Mockery.Of( out IMockable mocked );

                Assert.IsType<Mock<IMockable>>( actual );
                Assert.Equal( MockBehavior.Strict, actual.Behavior );
                Assert.Same( actual.Object, mocked );
            }
        }
    }
}
