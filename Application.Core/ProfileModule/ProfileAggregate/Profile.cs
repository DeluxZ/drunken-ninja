using Application.Common;
using Application.Core.ProfileModule.ProfileAddressAggregate;
using Application.Core.ProfileModule.ProfilePhoneAggregate;
using Application.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.ProfileAggregate
{
    public class Profile : Entity, IValidatableObject
    {
        [Key]
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<ProfileAddress> ProfileAddresses { get; set; }
        public virtual ICollection<ProfilePhone> ProfilePhones { get; set; }

        public Profile()
        {
            this.ProfileAddresses = new HashSet<ProfileAddress>();
            this.ProfilePhones = new HashSet<ProfilePhone>();
        }

        /// <summary>
        /// This will validate entity for all the conditions
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if(!String.IsNullOrWhiteSpace(this.FirstName)){

                validationResults.Add(new ValidationResult(
                    Messages.validation_ProfileFirstNameCannotBeNull,
                    new string[] { "FirstName" }
                ));
            }

            if (!String.IsNullOrWhiteSpace(this.LastName))
            {
                validationResults.Add(new ValidationResult(
                    Messages.validation_ProfileLastNameCannotBeBull,
                    new string[] { "LastName" }
                ));
            }

            if (!String.IsNullOrWhiteSpace(this.Email))
            {
                validationResults.Add(new ValidationResult(
                    Messages.validation_ProfileEmailCannotBeBull,
                    new string[] { "Email" }
                ));
            }

            return validationResults;
        }
    }
}
