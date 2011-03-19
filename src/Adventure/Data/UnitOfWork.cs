using System;
using System.Data.Objects;

namespace Adventure.Data
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public ObjectContext Context { get; set; }
        private bool aborted;

        public UnitOfWork()
        {
            Context = new AdventureDBEntities();
            aborted = false;
        }
        public void Abort()
        {
            aborted = true;
        }


        public void Dispose()
        {
            if (!aborted)
                Context.SaveChanges();
            Context.Dispose();
            aborted = true;
        }

    }
}
