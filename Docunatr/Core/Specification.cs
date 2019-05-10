using System;
using System.Linq;
using System.Linq.Expressions;

namespace Docunatr.Core
{
    /// <summary>
    /// The purpose of the Specification pattern is to keep knowledge about business rules
    /// contained in a single place. Using lambdas directly tends to scatter knowledge
    /// throughout the application.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T candidate)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(candidate);
        }

        public static implicit operator Func<T, bool>(Specification<T> specification)
        {
            return specification.ToExpression().Compile();
        }

        public Specification<T> And(Specification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public Specification<T> Or(Specification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public Specification<T> Xor(Specification<T> other)
        {
            return new XorSpecification<T>(this, other);
        }
    }
}