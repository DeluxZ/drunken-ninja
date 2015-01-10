using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.ProfileModule
{
    public class ContactForm
    {
        public List<AddressTypeDTO> AddressTypeDTO { get; set; }
        public List<PhoneTypeDTO> PhoneTypeDTO { get; set; }
    }
}
