using System;
using System.Linq.Expressions;
using Application.Core.Specification.Contract;
using Application.Core.Specification.Common;

namespace Application.Core.Specification
{
    /// <summary>
    /// A logic OR specification
    /// </summary>
    /// <typeparam name="T">Type of entity that check this specification</typeparam>
    public sealed class OrSpecification<T> : CompositeSpecification<T> where T : class
    {
        private ISpecification<T> _rightSideSpecification = null;
        private ISpecification<T> _leftSideSpecification = null;

        /// <summary>
        /// Default constructor for OrSpecification
        /// </summary>
        /// <param name="leftSide"></param>
        /// <param name="rightSide"></param>
        public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            if (leftSide == (ISpecification<T>)null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == (ISpecification<T>)null)
                throw new ArgumentNullException("rightSide");

            this._leftSideSpecification = leftSide;
            this._rightSideSpecification = rightSide;
        }

        public override ISpecification<T> LeftSideSpecification
        {
            get
            {
                return _leftSideSpecification;
            }
        }

        public override ISpecification<T> RightSideSpecification
        {
            get
            {
                return _rightSideSpecification;
            }
        }

        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            Expression<Func<T, bool>> left = _leftSideSpecification.SatisfiedBy();
            Expression<Func<T, bool>> right = _rightSideSpecification.SatisfiedBy();

            return (left.Or(right));
        }
    }
}
