namespace Moq
{
    /// <summary>
    /// Helper methods for working with Moq.
    /// </summary>

    public static class Mockery
    {
        /// <summary>
        /// Returns a mocked instance of the specified type.
        /// </summary>
        /// <typeparam name="T">Type to mock.</typeparam>
        /// <param name="behavior">Mock behavior. Defaults to Strict.</param>

        public static T Of<T>( MockBehavior behavior = MockBehavior.Strict ) where T : class =>
            new Mock<T>( behavior ).Object;

        /// <summary>
        /// Returns a mock of the specified type.
        /// </summary>
        /// <typeparam name="T">Type to mock.</typeparam>
        /// <param name="behavior">Mock behavior.</param>
        /// <param name="mocked">Instance of the mocked type.</param>

        public static Mock<T> Of<T>( MockBehavior behavior, out T mocked ) where T : class
        {
            var mock = new Mock<T>( behavior );
            mocked = mock.Object;
            return mock;
        }

        /// <summary>
        /// Returns a strict mock of the specified type.
        /// </summary>
        /// <typeparam name="T">Type to mock.</typeparam>
        /// <param name="mocked">Instance of the mocked type.</param>

        public static Mock<T> Of<T>( out T mocked ) where T : class => Of( MockBehavior.Strict, out mocked );
    }
}
