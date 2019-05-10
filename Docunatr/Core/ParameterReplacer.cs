using System.Linq.Expressions;

namespace Docunatr.Core
{
    /// <summary>
    /// Handles the passing of the parameter into the expression
    /// </summary>
    /// <see cref="https://github.com/vkhorikov/SpecificationPattern/blob/master/SpecificationPattern/ParameterReplacer.cs" />
    /// <typeparam name="T"></typeparam>
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
            => base.VisitParameter(_parameter);

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }
}