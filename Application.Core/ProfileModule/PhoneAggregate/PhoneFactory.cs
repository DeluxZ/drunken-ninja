using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.PhoneAggregate
{
    /// <summary>
    /// This is the factory for Phone creation
    /// </summary>
    public static class PhoneFactory
    {
        public static Phone CreatePhone(string number, string createdBy, DateTime created, string updatedBy, DateTime updated)
        {
            Phone phone = new Phone
            {
                Number = number,
                CreatedBy = createdBy,
                Created = created,
                UpdatedBy = updatedBy,
                Updated = updated
            };

            return phone;
        }
    }
}
