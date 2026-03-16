namespace BusinessLogic.Interfaces
{
    public interface IBaseService<TDomainModel, TDTO>
    {
        /// <summary>
        /// Asynchronously retrieves all records in database table.
        /// </summary>
        /// <returns>List of all the records in the specified table in database.</returns>
        Task<List<TDTO>?> GetAllAsync();


        /// <summary>
        /// Asynchronously retrieves a record by its primary key.
        /// </summary>
        /// <param name="id">Primary key of the target record.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the record if found; otherwise, <c>null</c>.
        /// </returns>
        Task<TDTO?> GetByIdAsync(Guid id);



        /// <summary>
        /// Asynchronously inserts a new record in the database.
        /// </summary>
        /// <param name="domainModel">The entity to be inserted.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result indicates whether the insertion succeeded.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="domainModel"/> is null.
        /// </exception>
        Task<bool> AddNewAsync(TDTO domainModel);



        /// <summary>
        /// Asynchronously updates the argument record in database.
        /// </summary>
        /// <param name="domainModel">Entity that is required to be updated</param>
        /// <returns>A value indicating whether the update operation succeeded.</returns>
        Task<bool> UpdateAsync(TDTO domainModel);



        /// <summary>
        /// Asynchronously changes the state of the record in database.
        /// either soft deletes it (change state to 1) or restores it (change state to 0).
        /// </summary>
        /// <param name="id">The unique identifier of the record to chnage its state.</param>
        /// <returns>A value indicating whether the changing the state succeeded.</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
