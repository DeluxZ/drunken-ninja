using Application.Common;
using Application.Core.ProfileModule.ProfileAddressAggregate;
using Application.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.AddressAggregate
{
    public class Address : Entity, IValidatableObject
    {
        [Key]
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<ProfileAddress> ProfileAddresses { get; set; }

        public Address()
        {
            this.ProfileAddresses = new HashSet<ProfileAddress>();
        }

        /// <summary>
        /// This will validate entity for all conditions
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (String.IsNullOrWhiteSpace(this.AddressLine1))
            {
                validationResults.Add(new ValidationResult(
                    Messages.validation_AddressAddressLine1CannotBeNull,
                    new string[] { "AddressLine1" }
                ));
            }

            if (String.IsNullOrWhiteSpace(this.AddressLine2))
            {
                validationResults.Add(new ValidationResult(
                    Messages.validation_AddressAddressLine2CannotBeBull,
                    new string[] { "AddressLine2" }
                ));
            }

            if (String.IsNullOrWhiteSpace(this.City))
            {
                validationResults.Add(new ValidationResult(
                    Messages.validation_AddresscityCannotBeEmpty,
                    new string[] { "City" }
                ));
            }

            if (String.IsNullOrWhiteSpace(this.Country))
            {
                validationResults.Add(new ValidationResult(
                    Messages.validation_AddressCountryCannotBeEmpty,
                    new string[] { "Country" }
                ));
            }

            if (String.IsNullOrWhiteSpace(this.State))
            {
                validationResults.Add(new ValidationResult(
                    Messages.validation_AddressStateCannotBeEmpty,
                    new string[] { "State" }
                ));
            }

            if (String.IsNullOrWhiteSpace(this.ZipCode))
            {
                validationResults.Add(new ValidationResult(
                    Messages.validation_ZipCodeCannotBeBull,
                    new string[] { "ZipCode" }
                ));
            }

            return validationResults;
        }
    }
}
