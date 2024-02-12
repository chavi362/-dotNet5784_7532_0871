namespace BlApi
{
    /// <summary>
    /// Interface for the business logic layer providing engineer-related functionality.
    /// </summary>
    public interface IEngineer
    {
        /// <summary>
        /// Creates a new engineer.
        /// </summary>
        /// <param name="item">The engineer to be created.</param>
        /// <returns>The ID of the newly created engineer.</returns>
        public int Create(BO.Engineer item);

        /// <summary>
        /// Updates an existing engineer.
        /// </summary>
        /// <param name="item">The updated engineer information.</param>
        /// <returns>The ID of the updated engineer.</returns>
        public int Update(BO.Engineer item);

        /// <summary>
        /// Deletes an engineer by ID.
        /// </summary>
        /// <param name="id">The ID of the engineer to delete.</param>
        /// <returns>The ID of the deleted engineer.</returns>
        public int Delete(int id);

        /// <summary>
        /// Retrieves an engineer by ID.
        /// </summary>
        /// <param name="id">The ID of the engineer to retrieve.</param>
        /// <returns>The engineer information if found, otherwise null.</returns>
        public BO.Engineer? Read(int id);

        /// <summary>
        /// Retrieves all engineers, optionally filtered by a provided predicate.
        /// </summary>
        /// <param name="filter">The filter predicate to apply, or null to retrieve all engineers.</param>
        /// <returns>A collection of engineers.</returns>
        public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
    }
}
