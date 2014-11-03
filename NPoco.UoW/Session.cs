namespace NPoco.UoW
{
    using System;

    /// <summary>
    /// Very basic NPoco Unit Of Work Implementation.  Sessions should be as short lived as possible, but are very cheap, so don't be afraid
    /// to use lots of them. Every update or save should be wrapped in a session (implemented through the Context IContext implementation).
    /// </summary>
    public sealed class Session : ISession
    {
        private readonly IDatabase _database;
        private readonly ITransaction _transaction;

        public Session(IDatabase db)
        {
            _database = db;
            _transaction = _database.GetTransaction();
        }

        public IDatabase Database
        {
            get
            {
                return _database;
            }
        }

        public string SessionId
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public void SaveChanges()
        {
            _transaction.Complete();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
