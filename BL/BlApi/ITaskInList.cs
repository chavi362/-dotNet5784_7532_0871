namespace BlApi
{
    /// <summary>
    /// Interface for the business logic layer providing functionality related to tasks in a list.
    /// </summary>
    public interface ITaskInList
    {
        /// <summary>
        /// Retrieves all tasks in a list, optionally filtered by a provided predicate.
        /// </summary>
        /// <param name="filter">The filter predicate to apply, or null to retrieve all tasks.</param>
        /// <returns>A collection of tasks in a list.</returns>
        public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null);
    }
}
