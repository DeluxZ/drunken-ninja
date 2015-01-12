using Application.Core.ProfileModule.AddressAggregate;
using Application.Core.ProfileModule.PhoneAggregate;
using Application.Core.ProfileModule.ProfileAggregate;
using Application.DTO.ProfileModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Manager.Conversion
{
    public static class Mapping
    {
        public static ProfileDTO ProfileToProfileDTO(Profile profile, List<AddressType> addressTypes, List<PhoneType> phoneTypes)
        {
            ProfileDTO objProfileDTO = new ProfileDTO
            {
                ProfileId = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                AddressDTO = new List<AddressDTO>(),
                PhoneDTO = new List<PhoneDTO>()
            };

            foreach (var profileAddress in profile.ProfileAddresses)
            {
                AddressDTO objAddressDTO = AddressToAddressDTO(profileAddress.Address);
                objAddressDTO.AddressTypeId = profileAddress.AddressTypeId;

                objProfileDTO.AddressDTO.Add(objAddressDTO);
            }

            foreach (var profilePhone in profile.ProfilePhones)
            {
                PhoneDTO objPhoneDTO = PhoneToPhoneDTO(profilePhone.Phone);
                objPhoneDTO.PhoneTypeId = profilePhone.PhoneTypeId;

                objProfileDTO.PhoneDTO.Add(objPhoneDTO);
            }

            return objProfileDTO;
        }

        public static AddressDTO AddressToAddressDTO(Address address)
        {
            AddressDTO objAddressDTO = new AddressDTO
            {
                AddressId = address.AddressId,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                ZipCode = address.ZipCode,
                Country = address.Country,
                State = address.State,
                City = address.City
            };

            return objAddressDTO;
        }

        public static List<AddressTypeDTO> AddressTypeToAddressTypeDTO(List<AddressType> addressTypes)
        {
            List<AddressTypeDTO> addressTypeDtos = new List<AddressTypeDTO>();

            foreach (AddressType addressType in addressTypes)
            {
                AddressTypeDTO addressTypeDto = new AddressTypeDTO
                {
                    AddressTypeId = addressType.AddressTypeId,
                    Name = addressType.Name
                };

                addressTypeDtos.Add(addressTypeDto);
            }

            return addressTypeDtos;
        }

        public static List<PhoneTypeDTO> PhoneTypeToPhoneTypeDTO(List<PhoneType> phoneTypes)
        {
            List<PhoneTypeDTO> phoneTypeDtos = new List<PhoneTypeDTO>();

            foreach (PhoneType phoneType in phoneTypes)
            {
                PhoneTypeDTO phoneTypeDto = new PhoneTypeDTO
                {
                    PhoneTypeId = phoneType.PhoneTypeId,
                    Name = phoneType.Name
                };

                phoneTypeDtos.Add(phoneTypeDto);
            }

            return phoneTypeDtos;
        }

        public static PhoneDTO PhoneToPhoneDTO(Phone phone)
        {
            PhoneDTO objPhoneDTO = new PhoneDTO();

            if (phone != null)
            {
                objPhoneDTO.PhoneId = phone.PhoneId;
                objPhoneDTO.Number = phone.Number;
            }

            return objPhoneDTO;
        }
    }
}
