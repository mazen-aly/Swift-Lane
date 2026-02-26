namespace DataAccess.Interfaces
{
    public interface IRepository<TDomainModel>
    {
        /// <summary>
        /// Asynchronously get all records in database table.
        /// </summary>
        /// <returns>List of all the records in the specified table in database.</returns>
        Task<List<TDomainModel>> GetAllAsync();


        /// <summary>
        /// Asynchronously retrieves a record by its primary key.
        /// </summary>
        /// <param name="id">Primary key of the target record.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the record if found; otherwise, <c>null</c>.
        /// </returns>
        Task<TDomainModel?> GetByIdAsync(Guid id);


        /// <summary>
        /// Asynchronously inserts a new record in the database.
        /// </summary>
        /// <param name="newDomainModel">The new entity to be inserted into a database table.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result indicates whether the insertion succeeded.
        /// </returns>
        Task<bool> AddNewAsync(TDomainModel newDomainModel);


        /// <summary>
        /// Asynchronously updates the argument record in database.
        /// </summary>
        /// <param name="updatedDomainModel">Entity that is required to be updated in database</param>
        /// <returns>A value indicating whether the update operation succeeded.</returns>
        Task<bool> UpdateAsync(TDomainModel updatedDomainModel);


        /// <summary>
        /// Asynchronously checks the existence of record in database according to its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the entity whose existence is required to be checked.</param>
        /// <returns>A value indicating whether the record with the given ID exists.</returns>
        Task<bool> CheckExistenceAsync(Guid id);
    }
}
