using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Docunatr.Core
{
    /// <summary>
    /// A static member in a generic type will not be shared among instances of different close constructed types.
    /// This class exists so the same BackingStore is available to all Repository<T> implementors.
    /// </summary>
    public class SerializingRepositoryBase
    {
        protected static readonly Dictionary<Guid, string> BackingStore = new Dictionary<Guid, string>();
    }

    /// <summary>
    /// This was the first repository I created. Serialization for storage is slow -- it's about 3/4 as fast as the Tuple-based repository. It's still here for learning purposes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete]
    public class SerializingRepository<T> : SerializingRepositoryBase, IRepository<T> where T : IEntity
    {
        public void Empty()
        {
            BackingStore.Clear();
        }

        public void Store(T item)
        {
            BackingStore.Add(item.Id, JsonConvert.SerializeObject(item));
        }

        public T Retrieve(Guid id)
        {
            return JsonConvert.DeserializeObject<T>(BackingStore[id]);
        }

        public IEnumerable<T> Retrieve(Specification<T> specification)
        {
            // TODO: reconsider backing store -- this deserialization is a slow operation
            return BackingStore.Values.Select(JsonConvert.DeserializeObject<T>).Where(specification);
        }
    }
}