using Application.Common;
using Application.Core.ProfileModule.ProfileAddressAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.AddressAggregate
{
    public class AddressType : Entity
    {
        [Key]
        public int AddressTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProfileAddress> ProfileAddresses { get; set; }

        public AddressType()
        {
            this.ProfileAddresses = new HashSet<ProfileAddress>();
        }
    }
}
