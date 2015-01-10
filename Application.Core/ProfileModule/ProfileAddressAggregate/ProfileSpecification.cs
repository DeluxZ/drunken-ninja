using Application.Core.ProfileModule.ProfileAggregate;
using Application.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.ProfileAddressAggregate
{
    /// <summary>
    /// A list of Profile specification
    /// </summary>
    public static class ProfileSpecification
    {
        public static Specification<Profile> GetProfileByFilter(string firstName, string lastName, string email)
        {
            Specification<Profile> specProfile = new TrueSpecification<Profile>();

            if (!String.IsNullOrEmpty(firstName))
                specProfile &= new DirectSpecification<Profile>(p => p.FirstName.Contains(firstName));

            if (!String.IsNullOrEmpty(lastName))
                specProfile &= new DirectSpecification<Profile>(p => p.LastName.Contains(lastName));

            if (!String.IsNullOrEmpty(email))
                specProfile &= new DirectSpecification<Profile>(p => p.Email.Contains(email));

            return specProfile;
        }
    }
}
