namespace BlApi
{
    /// <summary>
    /// Interface for the business logic layer providing task-related functionality.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="item">The task to be created.</param>
        /// <returns>The ID of the newly created task.</returns>
        public int Create(BO.Task item);

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="item">The updated task information.</param>
        public void Update(BO.Task item);

        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>The ID of the deleted task.</returns>
        public int Delete(int id);

        /// <summary>
        /// Retrieves a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>The task information if found, otherwise null.</returns>
        public BO.Task? Read(int id);

        /// <summary>
        /// Retrieves all tasks, optionally filtered by a provided predicate.
        /// </summary>
        /// <param name="filter">The filter predicate to apply, or null to retrieve all tasks.</param>
        /// <returns>A collection of tasks.</returns>
        public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null);
    }
}
