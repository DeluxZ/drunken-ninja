using Application.Core.Specification.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Application.Core.Specification.Implementation
{
    /// <summary>
    /// Represent a Expression Specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Specification<T> : ISpecification<T> where T : class
    {
        /// <summary>
        /// IsSatisfied Specification pattern method
        /// </summary>
        /// <returns>Expression that satisfy this specification</returns>
        public abstract Expression<Func<T, bool>> SatisfiedBy();

        /// <summary>
        /// And operator
        /// </summary>
        /// <param name="leftSideSpecification">left operand in this AND operation</param>
        /// <param name="rightSideSpecification">right operand in this AND operation</param>
        /// <returns>New specification</returns>
        public static Specification<T> operator &(Specification<T> leftSideSpecification, Specification<T> rightSideSpecification)
        {
            return new AndSpecification<T>(leftSideSpecification, rightSideSpecification);
        }

        /// <summary>
        /// Or operator
        /// </summary>
        /// <param name="leftSideSpecification">left operand in this OR operation</param>
        /// <param name="rightSideSpecification">right operand in this OR operation</param>
        /// <returns>New specification</returns>
        public static Specification<T> operator |(Specification<T> leftSideSpecification, Specification<T> rightSideSpecification)
        {
            return new OrSpecification<T>(leftSideSpecification, rightSideSpecification);
        }

        /// <summary>
        /// Not specification
        /// </summary>
        /// <param name="specification">Specification to negate</param>
        /// <returns>New specification</returns>
        public static Specification<T> operator !(Specification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }

        /// <summary>
        /// Override operator false, only for support AND OR operators
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See False operator in C#</returns>
        public static bool operator false(Specification<T> specification)
        {
            return false;
        }

        /// <summary>
        /// Override operator true, only for support AND OR operators
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>See True operator in C#</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParamters", MessageId = "specification")]
        public static bool operator true(Specification<T> specification)
        {
            return true;
        }
    }
}
