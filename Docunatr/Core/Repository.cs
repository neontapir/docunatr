using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Docunatr.Core
{
    public class Repository<T> where T : IEntity
    {
        private static readonly Dictionary<Guid, string> BackingStore = new Dictionary<Guid, string>();

        public void Store(T item)
        {
            BackingStore.Add(item.Id, JsonConvert.SerializeObject(item));
        }

        public T Retrieve(Guid id)
        {
            return JsonConvert.DeserializeObject<T>(BackingStore[id]);
        }
    }
}