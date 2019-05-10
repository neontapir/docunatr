using System;
using System.Collections.Generic;

namespace Docunatr.Core
{
    public interface IRepository<T> where T : IEntity
    {
        void Empty();
        void Store(T item);
        T Retrieve(Guid id);
        IEnumerable<T> Retrieve(Specification<T> specification);
    }
}