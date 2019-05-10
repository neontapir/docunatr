using System;
using System.Collections.Generic;
using System.Linq;
using Docunatr.Specifications;

namespace Docunatr.Core
{
    /// <summary>
    /// A static member in a generic type will not be shared among instances of different close constructed types.
    /// This class exists so the same BackingStore is available to all Repository<T> implementors.
    /// </summary>
    public class TupleRepositoryBase
    {
        public class StoredEntity : Tuple<Type, object>
        {
            public StoredEntity(Type item1, object item2) : base(item1, item2)
            {
            }

            public Type Type => Item1;
            public object Value => Item2;
        }

        protected static readonly List<StoredEntity> BackingStore = new List<StoredEntity>();
    }

    public class Repository<T> : TupleRepositoryBase, IRepository<T> where T : IEntity
    {
        public void Empty()
        {
            BackingStore.Clear();
        }

        public void Store(T item)
        {
            BackingStore.Add(new StoredEntity(typeof(T), item));
        }

        public T Retrieve(Guid id)
        {
            return Retrieve(new IdSpecification<T>(id)).SingleOrDefault();
        }

        public IEnumerable<T> Retrieve(Specification<T> specification)
        {
            // TODO: reconsider backing store -- this type casting is a slow operation
            return BackingStore.Select(x => Convert.ChangeType(x.Value, x.Type)).OfType<T>().Where(specification);
        }
    }
}