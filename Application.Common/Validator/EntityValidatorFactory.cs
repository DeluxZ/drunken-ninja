using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Validator
{
    /// <summary>
    /// Entity Validator Factory
    /// </summary>
    public static class EntityValidatorFactory
    {
        static IEntityValidatorFactory _factory = null;

        /// <summary>
        /// Set the entity validator to use
        /// </summary>
        /// <param name="factory">Entity Validator to use</param>
        public static void SetCurrent(IEntityValidatorFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Create a new validator
        /// </summary>
        /// <returns>Created EntityValidator</returns>
        public static IEntityValidator CreateValidator()
        {
            return (_factory != null) ? _factory.Create() : null;
        }
    }
}
