using Application.Common;
using Application.Core.ProfileModule.PhoneAggregate;
using Application.Core.ProfileModule.ProfileAggregate;
using Application.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.ProfilePhoneAggregate
{
    public class ProfilePhone : Entity, IValidatableObject
    {
        [Key]
        public int ProfilePhoneId { get; set; }
        public int ProfileId { get; set; }
        public int PhoneId { get; set; }
        public int PhoneTypeId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Phone Phone { get; set; }
        public virtual PhoneType PhoneType { get; set; }
        public virtual Profile Profile { get; set; }

        /// <summary>
        /// Associate existing Profile to this ProfilePhone
        /// </summary>
        /// <param name="profile"></param>
        public void AssociateProfileForThisProfilePhone(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(Messages.exception_ProfilePhoneCannotAssociateNullProfile);
            }

            // Fix relation
            this.ProfileId = profile.ProfileId;
            this.Profile = profile;
        }

        /// <summary>
        /// Set the Profile reference for this ProfilePhone
        /// </summary>
        /// <param name="profileId"></param>
        public void SetTheProfileReference(int profileId)
        {
            if(ProfileId != 0)
            {
                // Fix relation
                this.ProfileId = profileId;
                this.Profile = null;
            }
        }

        /// <summary>
        /// Associate existing Phone to this ProfilePhone
        /// </summary>
        /// <param name="phone"></param>
        public void AssociatePhoneForThisProfilePhone(Phone phone)
        {
            if (phone == null)
            {
                throw new ArgumentNullException(Messages.exception_ProfilePhoneCannotAssociateNullPhone);
            }

            // Fix relation
            this.PhoneId = phone.PhoneId;
            this.Phone = phone;
        }

        /// <summary>
        /// Set the Phone reference for this ProfilePhone
        /// </summary>
        /// <param name="phoneId"></param>
        public void SetThePhoneReference(int phoneId)
        {
            if (phoneId != 0)
            {
                // Fix relation
                this.PhoneId = phoneId;
                this.Phone = null;
            }
        }

        /// <summary>
        /// Associate existing PhoneType to this ProfilePhone
        /// </summary>
        /// <param name="phoneType"></param>
        public void AssociatePhoneTypeForThisProfilePhone(PhoneType phoneType)
        {
            if (phoneType == null)
            {
                throw new ArgumentNullException(Messages.exception_ProfilePhoneCannotAssociateNullPhoneType);
            }

            // Fix relation
            this.PhoneTypeId = phoneType.PhoneTypeId;
            this.PhoneType = phoneType;
        }

        /// <summary>
        /// Set the PhoneType reference for this ProfilePhone
        /// </summary>
        /// <param name="phoneTypeId"></param>
        public void SetThePhoneTypeReference(int phoneTypeId)
        {
            if (phoneTypeId != 0)
            {
                // Fix relation
                this.PhoneTypeId = phoneTypeId;
                this.PhoneType = null;
            }
        }

        /// <summary>
        /// This will validate entity for all the conditions
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (this.ProfileId == 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProfilePhoneProfileIDCannotBeEmpty, new string[] { "ProfileId" }));

            if (this.PhoneId == 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProfilePhonePhoneIDCannotBeEmpty, new string[] { "PhoneId" }));

            if (this.PhoneTypeId == 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProfilePhonePhoneTypeIDCannotBeEmpty, new string[] { "PhoneTypeId" }));

            return validationResults;
        }
    }
}
