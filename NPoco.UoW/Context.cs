namespace NPoco.UoW
{
    public class Context : IContext
    {
        readonly IDatabase _db;

        public Context(IDatabase db)
        {
            _db = db;
        }

        public ISession OpenSession()
        {
            Session session = new Session(_db);
            return session;
        }
    }
}
