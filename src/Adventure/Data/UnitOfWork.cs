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
        public void Save()
        {

            Context.SaveChanges();
        }

        public void Dispose()
        {
            if (!aborted)
               Save();
            Context.Dispose();
            aborted = true;
        }

    }
}
