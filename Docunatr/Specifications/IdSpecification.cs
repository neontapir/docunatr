using System;
using System.Linq.Expressions;
using Docunatr.Core;

namespace Docunatr.Specifications
{
    public class IdSpecification<T> : Specification<T> where T : IEntity
    {
        private readonly Guid _id;

        public IdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return item => item.Id == _id;
        }
    }
}