using Application.Core.ProfileModule.AddressAggregate;
using Application.Core.ProfileModule.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.ProfileAddressAggregate
{
    /// <summary>
    /// This is the factory for the ProfileAddress creation
    /// </summary>
    public static class ProfileAddressFactory
    {
        public static ProfileAddress ProfileAddress(Profile profile, Address address, AddressType addressType, string createdBy, DateTime created, string updatedBy, DateTime updated)
        {
            ProfileAddress profileAddress = new ProfileAddress
            {
                CreatedBy = createdBy,
                Created = created,
                UpdatedBy = updatedBy,
                Updated = updated,

                ProfileId = profile.ProfileId,
                AddressId = address.AddressId,
                AddressTypeId = addressType.AddressTypeId
            };

            return profileAddress;
        }
    }
}
