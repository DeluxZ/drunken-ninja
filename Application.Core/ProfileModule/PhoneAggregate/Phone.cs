using Application.Common;
using Application.Core.ProfileModule.ProfilePhoneAggregate;
using Application.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.PhoneAggregate
{
    public class Phone : Entity, IValidatableObject
    {
        [Key]
        public int PhoneId { get; set; }
        public string Number { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<ProfilePhone> ProfilePhones { get; set; }

        public Phone()
        {
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

            if (String.IsNullOrWhiteSpace(this.Number))
                validationResults.Add(new ValidationResult(Messages.validation_PhoneNumberCannotBeNull, new string[] { "Number" }));

            return validationResults;
        }
    }
}
