namespace BlApi
{
    /// <summary>
    /// Factory class for creating instances of the business logic layer.
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Gets an instance of the business logic layer.
        /// </summary>
        /// <returns>An instance of the business logic layer.</returns>
        public static IBl Get() => new BlImplementation.Bl();
    }
}
