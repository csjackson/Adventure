using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace Adventure.Data
{
    // CRUD: Create, Read, Update, Delete
    public interface IRepository<T> : IDisposable
            where T : EntityObject
    {
        void Add(T entity);
        void Delete(T entity);
        IQueryable<T> AsQueryable();
    }
    public class Repository<T> : IDisposable, IRepository<T>
        where T : EntityObject
    {
        private IUnitOfWork uow;
        private ObjectSet<T> _objectSet;
        private ObjectSet<T> objectSet
        {
            get
            {
                if (_objectSet == null)
                    _objectSet = uow.Context.CreateObjectSet<T>();
                return _objectSet;
            }
        }

        public Repository(IUnitOfWork uow)
        {
            this.uow = uow;

        }
        public void Add(T entity) // create
        {
            objectSet.AddObject(entity);
        }
        public void Delete(T entity) // Delete
        {
            objectSet.DeleteObject(entity);
        }
        public IQueryable<T> AsQueryable() // Read & Update
        {
            return objectSet;
        }


        public void Dispose()
        {
            uow.Dispose();
        }

    }
}
