using System;
using System.Linq.Expressions;

namespace Docunatr.Core
{
    /// <summary>
    /// Combine two specifications, true if either are true
    /// </summary>
    /// <see cref="https://github.com/vkhorikov/SpecificationPattern/blob/master/SpecificationPattern/Specifications.cs" />
    /// <typeparam name="T"></typeparam>
    public class NotSpecification<T> : Specification<T> {
        private readonly Specification<T> _left;

        public NotSpecification(Specification<T> left) {
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression() {
            var leftExpression = _left.ToExpression();
            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.Not(leftExpression.Body);
            exprBody = (UnaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

            return finalExpr;
        }
    }
}