using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specification.Common
{
    /// <summary>
    /// Helper for rebinder parameters without use Invoke method in expressions
    /// (these methods are not supported in all linq query providers, for example in 
    /// Linq2Entities is not supported)
    /// </summary>
    public sealed class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="map">Map specification</param>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// Replace parameters in expression with a Map information
        /// </summary>
        /// <param name="map">Map information</param>
        /// <param name="exp">Expression to relace parameters</param>
        /// <returns>Expression with parameters replaced</returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        /// <summary>
        /// Visit pattern method
        /// </summary>
        /// <param name="node">A parameter expression</param>
        /// <returns>New visited expression</returns>
        protected override Expression VisitParameter(ParameterExpression parameterExpression)
        {
            ParameterExpression replacement;
            if(map.TryGetValue(parameterExpression,out replacement))
            {
                parameterExpression = replacement;
            }
            return base.VisitParameter(parameterExpression);
        }
    }
}
