using Application.Common;
using Application.Core.ProfileModule.ProfilePhoneAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.PhoneAggregate
{
    public class PhoneType : Entity
    {
        [Key]
        public int PhoneTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProfilePhone> ProfilePhones { get; set; }

        public PhoneType()
        {
            this.ProfilePhones = new HashSet<ProfilePhone>();
        }
    }
}
