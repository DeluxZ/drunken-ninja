﻿using Application.Core.Specification.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Application.Core.Specification
{
    /// <summary>
    /// True specification
    /// </summary>
    /// <typeparam name="T">Type of entity in this specification</typeparam>
    public sealed class TrueSpecification<T> : Specification<T> where T : class
    {
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            // Create "result variable"transform adhoc execution plan in prepared plan
            // for more info: http://geeks.ms/blogs/unai/2010/07/91/ef-4-0-performance-tips-1.aspx
            bool result = true;
            Expression<Func<T, bool>> trueExpression = t => result;

            return trueExpression;
        }
    }
}
