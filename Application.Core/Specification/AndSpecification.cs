using Application.Core.Specification.Contract;
using Application.Core.Specification.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Application.Core.Specification
{
    /// <summary>
    /// A logic AND Specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AndSpecification<T> : CompositeSpecification<T> where T : class
    {
        private ISpecification<T> _rightSideSpecification = null;
        private ISpecification<T> _leftSideSpecification = null;

        /// <summary>
        /// Default constructor for AndSpecification
        /// </summary>
        /// <param name="leftSide"></param>
        /// <param name="rightSide"></param>
        public AndSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            if (leftSide == (ISpecification<T>)null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == (ISpecification<T>)null)
                throw new ArgumentNullException("rightSide");

            this._leftSideSpecification = leftSide;
            this._rightSideSpecification = rightSide;
        }

        /// <summary>
        /// Left side specification
        /// </summary>
        public override ISpecification<T> LeftSideSpecification
        {
            get
            {
                return _leftSideSpecification;
            }
        }

        /// <summary>
        /// Right side specification
        /// </summary>
        public override ISpecification<T> RightSideSpecification
        {
            get
            {
                return _rightSideSpecification;
            }
        }

        /// <summary>
        /// Implementation for method SatisfiedBy
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            Expression<Func<T, bool>> left = _leftSideSpecification.SatisfiedBy();
            Expression<Func<T, bool>> right = _rightSideSpecification.SatisfiedBy();

            return (left.And(right));
        }
    }
}
