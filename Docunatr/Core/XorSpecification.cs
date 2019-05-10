using System;
using System.Linq.Expressions;

namespace Docunatr.Core
{
    /// <summary>
    /// Combine two specifications, true if either are true
    /// </summary>
    /// <see cref="https://github.com/vkhorikov/SpecificationPattern/blob/master/SpecificationPattern/Specifications.cs" />
    /// <typeparam name="T"></typeparam>
    public class XorSpecification<T> : Specification<T> {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public XorSpecification(Specification<T> left, Specification<T> right) {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression() {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();
            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            var notBothSpecification = (_left.And(_right).Not());
            exprBody = Expression.AndAlso(exprBody, notBothSpecification.ToExpression().Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

            return finalExpr;
        }
    }
}