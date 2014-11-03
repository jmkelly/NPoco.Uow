using System;

namespace NPoco.UoW
{
    public interface ISession : IDisposable
    {
        string SessionId { get; }

        IDatabase Database { get; }

        void SaveChanges();
    }
}
