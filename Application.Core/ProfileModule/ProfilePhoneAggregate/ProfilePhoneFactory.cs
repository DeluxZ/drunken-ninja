using Application.Core.ProfileModule.PhoneAggregate;
using Application.Core.ProfileModule.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.ProfilePhoneAggregate
{
    public static class ProfilePhoneFactory
    {
        /// <summary>
        /// Create a new ProfilePhone
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="phone"></param>
        /// <param name="phoneType"></param>
        /// <param name="createdBy"></param>
        /// <param name="dateTime1"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        public static ProfilePhone CreateProfilePhone(Profile profile, Phone phone, PhoneType phoneType, string createdBy, DateTime created, string updatedBy, DateTime updated)
        {
            ProfilePhone profilePhone = new ProfilePhone
            {
                Created = created,
                CreatedBy = createdBy,
                Updated = updated,
                UpdatedBy = updatedBy,

                ProfileId = profile.ProfileId,
                PhoneId = phone.PhoneId,
                PhoneTypeId = phoneType.PhoneTypeId
            };

            return profilePhone;
        }
    }
}
