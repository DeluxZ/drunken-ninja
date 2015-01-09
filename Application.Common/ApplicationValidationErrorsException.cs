using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    /// <summary>
    /// The custom exception for validation errors
    /// </summary>
    public class ApplicationValidationErrorsException : Exception
    {
        IEnumerable<string> _validationErrors;
        /// <summary>
        /// Get the validation errors messages
        /// </summary>
        public IEnumerable<string> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        /// <summary>
        /// Create a new instance of Application validation errors exception
        /// </summary>
        /// <param name="validationErrors"></param>
        public ApplicationValidationErrorsException(IEnumerable<string> validationErrors)
            : base("Invalid type, excepted is RegisterTypesMapConfigurationElement")
        {
            _validationErrors = validationErrors;
        }
    }
}
