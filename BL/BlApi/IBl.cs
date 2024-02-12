using BO;

namespace BlApi
{
    /// <summary>
    /// Interface for the business logic layer, providing access to various entities.
    /// </summary>
    public interface IBl
    {
        /// <summary>
        /// Gets the task-related functionality.
        /// </summary>
        public ITask Task { get; }

        /// <summary>
        /// Gets the engineer-related functionality.
        /// </summary>
        public IEngineer Engineer { get; }

        /// <summary>
        /// Gets the milestone-related functionality.
        /// </summary>
        public IMilestone Milestone { get; }

        /// <summary>
        /// Gets the task-in-list-related functionality.
        /// </summary>
        public ITaskInList TaskInList { get; }
    }
}
