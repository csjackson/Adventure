using System;
using System.Data.Objects;

namespace Adventure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ObjectContext Context { get; set; }
        void Abort();
    }
}
