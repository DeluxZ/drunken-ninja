using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.AddressAggregate
{
    /// <summary>
    /// This is the factory for Address creation
    /// </summary>
    public static class AddressFactory
    {
        /// <summary>
        /// Create a new address object
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="zipCode"></param>
        /// <param name="createdBy"></param>
        /// <param name="created"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        public static Address CreateAddress(string line1, string line2, string city, string state, string country, string zipCode, 
            string createdBy, DateTime created, string updatedBy, DateTime updated)
        {
            Address address = new Address
            {
                AddressLine1 = line1,
                AddressLine2 = line2,
                City = city,
                State = state,
                Country = country,
                ZipCode = zipCode,
                CreatedBy = createdBy,
                Created = created,
                UpdatedBy = updatedBy,
                Updated = updated
            };

            return address;
        }
    }
}
