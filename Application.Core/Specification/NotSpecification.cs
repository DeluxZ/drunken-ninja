using Application.Core.Specification.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Application.Core.Specification.Contract;

namespace Application.Core.Specification
{
    /// <summary>
    /// A logic NOT Specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class NotSpecification<T> : Specification<T> where T : class
    {
        Expression<Func<T, bool>> _originalCriteria;

        /// <summary>
        /// Constructor for NotSpecification
        /// </summary>
        /// <param name="originalSpecification">Original specification</param>
        public NotSpecification(ISpecification<T> originalSpecification)
        {
            if (originalSpecification == (ISpecification<T>)null)
                throw new ArgumentNullException("originalSpecification");

            _originalCriteria = originalSpecification.SatisfiedBy();
        }

        public NotSpecification(Expression<Func<T,bool>> originalSpecification)
        {
            if (originalSpecification == (Expression<Func<T, bool>>)null)
                throw new ArgumentNullException("originalSpecification");

            _originalCriteria = originalSpecification;
        }

        /// <summary>
        /// Implementation for method SatisfiedBy
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(_originalCriteria.Body), _originalCriteria.Parameters.Single());
        }
    }
}
