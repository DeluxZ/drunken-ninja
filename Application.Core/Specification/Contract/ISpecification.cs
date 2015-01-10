using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specification.Contract
{
    /// <summary>
    /// Base contract for Specficiation pattern with Linq and lambda expression support
    /// Ref : http://martinfowler.com/apsupp/spec.pdf
    /// Ref : http://en.wikipedia.org/wiki/Specification_pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T> where T : class
    {
        /// <summary>
        /// Check if this specification is satisfied by a specific expression lambda
        /// </summary>
        /// <returns></returns>
        Expression<Func<T, bool>> SatisfiedBy();
    }
}
