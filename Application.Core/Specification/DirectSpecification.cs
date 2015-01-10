using Application.Core.Specification.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specification
{
    /// <summary>
    /// A Direct Specification is a simple implementation of specification
    /// that acquire this from a lambda expression in constructor
    /// </summary>
    public sealed class DirectSpecification<T> : Specification<T> where T : class
    {
        Expression<Func<T, bool>> _matchingCriteria;

        /// <summary>
        /// Default constructor for Direct Specification
        /// </summary>
        /// <param name="matchingCriteria">A Matching Criteria</param>
        public DirectSpecification(Expression<Func<T,bool>> matchingCriteria)
        {
            if (matchingCriteria == (Expression<Func<T, bool>>)null)
                throw new ArgumentNullException("matchingCriteria");

            _matchingCriteria = matchingCriteria;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            return _matchingCriteria;
        }
    }
}
