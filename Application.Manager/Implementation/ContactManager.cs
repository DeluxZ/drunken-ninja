using Application.Common;
using Application.Common.Logging;
using Application.Common.Validator;
using Application.Core.ProfileModule.AddressAggregate;
using Application.Core.ProfileModule.PhoneAggregate;
using Application.Core.ProfileModule.ProfileAddressAggregate;
using Application.Core.ProfileModule.ProfileAggregate;
using Application.Core.ProfileModule.ProfilePhoneAggregate;
using Application.DTO.ProfileModule;
using Application.Manager.Contract;
using Application.Manager.Conversion;
using Application.Manager.Resources;
using Application.Repository.ProfileModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Manager.Implementation
{
    public class ContactManager : IContactManager
    {
        private AddressRepository _addressRepository;
        private AddressTypeRepository _addressTypeRepository;
        private PhoneRepository _phoneRepository;
        private PhoneTypeRepository _phoneTypeRepository;
        private ProfileAddressRepository _profileAddressRepository;
        private ProfilePhoneRepository _profilePhoneRepository;
        private ProfileRepository _profileRepository;

        public ContactManager(AddressRepository addressRepository, AddressTypeRepository addressTypeRepository, PhoneRepository phoneRepository, PhoneTypeRepository phoneTypeRepository,
            ProfileAddressRepository profileAddressRepository, ProfilePhoneRepository profilePhoneRepository, ProfileRepository profileRepository)
        {
            if (addressRepository == null)
                throw new ArgumentNullException("addressRepository");

            if (addressTypeRepository == null)
                throw new ArgumentNullException("addressTypeRepository");

            if (phoneRepository == null)
                throw new ArgumentNullException("phoneRepository");

            if (phoneTypeRepository == null)
                throw new ArgumentNullException("phoneTypeRepository");

            if (profileAddressRepository == null)
                throw new ArgumentNullException("profileAddressRepository");

            if (profilePhoneRepository == null)
                throw new ArgumentNullException("profilePhoneRepository");

            if (profileRepository == null)
                throw new ArgumentNullException("profileRepository");

            _addressRepository = addressRepository;
            _addressTypeRepository = addressTypeRepository;
            _phoneRepository = phoneRepository;
            _phoneTypeRepository = phoneTypeRepository;
            _profileAddressRepository = profileAddressRepository;
            _profilePhoneRepository = profilePhoneRepository;
            _profileRepository = profileRepository;
        }

        /// <summary>
        /// Get all profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<ProfileDTO> FindProfiles(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            // Recover profiles in paged fashion
            List<Profile> profiles = _profileRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.Created, false).ToList();

            if (profiles != null && profiles.Any())
            {
                List<AddressType> addressTypes = _addressTypeRepository.GetAll().ToList();
                List<PhoneType> phoneTypes = _phoneTypeRepository.GetAll().ToList();
                List<ProfileDTO> profilesDto = new List<ProfileDTO>();

                foreach (var profile in profiles)
                {
                    profilesDto.Add(Mapping.ProfileToProfileDTO(profile, addressTypes, phoneTypes));
                }

                return profilesDto;
            }

            return new List<ProfileDTO>();
        }

        /// <summary>
        /// Delete profile
        /// </summary>
        /// <param name="profileId"></param>
        public void DeleteProfile(int profileId)
        {
            var profile = _profileRepository.Get(profileId);

            if (profile != null)
            {
                // Delete all addresses associated with this profile
                List<ProfileAddress> addresses = profile.ProfileAddresses.ToList();
                foreach (ProfileAddress address in addresses)
                {
                    this.DeleteProfileAddress(address);
                }

                // Delete all phones associated with this profile
                List<ProfilePhone> phones = profile.ProfilePhones.ToList();
                foreach (ProfilePhone phone in phones)
                {
                    this.DeleteProfilePhone(phone);
                }

                _profileRepository.Remove(profile);
                _profileRepository.UnitOfWork.Commit();
            }
            else
            {
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingProfile);
            }
        }

        /// <summary>
        /// Find profile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProfileDTO FindProfileById(int id)
        {
            Profile profile = _profileRepository.Get(id);

            if (profile != null)
            {
                List<AddressType> addressTypes = _addressTypeRepository.GetAll().ToList();
                List<PhoneType> phoneTypes = _phoneTypeRepository.GetAll().ToList();

                return Mapping.ProfileToProfileDTO(profile, addressTypes, phoneTypes);
            }

            return new ProfileDTO();
        }

        /// <summary>
        /// Get all initialization data for Contact page
        /// </summary>
        /// <returns></returns>
        public ContactForm InitializePageData()
        {
            List<AddressType> addressTypes = _addressTypeRepository.GetAll().ToList();
            List<PhoneType> phoneTypes = _phoneTypeRepository.GetAll().ToList();

            return new ContactForm
            {
                AddressTypeDTO = Mapping.AddressTypeToAddressTypeDTO(addressTypes),
                PhoneTypeDTO = Mapping.PhoneTypeToPhoneTypeDTO(phoneTypes)
            };
        }

        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        public void SaveProfileInformation(ProfileDTO profileDTO)
        {
            if (profileDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            var newProfile = ProfileFactory.CreateProfile(profileDTO.FirstName, profileDTO.LastName, profileDTO.Email, "Anand", DateTime.Now, "Anand", DateTime.Now);
            newProfile = SaveProfile(newProfile);

            if (profileDTO.AddressDTO != null && profileDTO.AddressDTO.Any())
            {
                foreach (AddressDTO address in profileDTO.AddressDTO)
                {
                    this.SaveAddress(address, newProfile);
                }
            }

            if (profileDTO.PhoneDTO != null && profileDTO.PhoneDTO.Any())
            {
                foreach (PhoneDTO phone in profileDTO.PhoneDTO)
                {
                    this.SavePhone(phone, newProfile);
                }
            }
        }

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        public void UpdateProfileInformation(int id, ProfileDTO profileDTO)
        {
            if (profileDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            Profile currentProfile = _profileRepository.Get(id);

            // Assign updated value to existing profile
            var updatedProfile = new Profile
            {
                ProfileId = id,
                FirstName = profileDTO.FirstName,
                LastName = profileDTO.LastName,
                Email = profileDTO.Email
            };

            // Update profile
            updatedProfile = this.UpdateProfile(currentProfile, updatedProfile);

            // Update Address
            List<AddressDTO> addresses = profileDTO.AddressDTO;
            List<ProfileAddress> currentAddresses = _profileAddressRepository.GetFiltered(x => x.ProfileId == id).ToList();

            UpdateAddress(addresses, currentAddresses, updatedProfile);

            // Update Phone
            List<PhoneDTO> phones = profileDTO.PhoneDTO;
            List<ProfilePhone> currentPhones = _profilePhoneRepository.GetFiltered(x => x.ProfileId == id).ToList();

            UpdatePhone(phones, currentPhones, updatedProfile);
        }

        /// <summary>
        /// Add new address
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <param name="profile"></param>
        private void SaveAddress(AddressDTO addressDTO, Profile profile)
        {
            if (addressDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            // Create a new Address entity
            Address newAddress = AddressFactory.CreateAddress(addressDTO.AddressLine1, addressDTO.AddressLine2, addressDTO.City, addressDTO.State,
                addressDTO.Country, addressDTO.ZipCode, "Anand", DateTime.Now, "Anand", DateTime.Now);
            SaveAddress(newAddress);

            AddressType addressType = _addressTypeRepository.Get(addressDTO.AddressTypeId);

            // Create a new Profile Address entity
            ProfileAddress newProfileAddress = ProfileAddressFactory.ProfileAddress(profile, newAddress, addressType, "Anand", DateTime.Now, "Anand", DateTime.Now);
            SaveProfileAddress(newProfileAddress);
        }

        /// <summary>
        /// Save Address
        /// </summary>
        /// <param name="newAddress"></param>
        /// <returns></returns>
        private Address SaveAddress(Address address)
        {
            IEntityValidator entityValidator = EntityValidatorFactory.CreateValidator();
            if (entityValidator.IsValid(address))
            {
                _addressRepository.Add(address);
                _addressRepository.UnitOfWork.Commit();

                return address;
            }

            throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(address));
        }

        /// <summary>
        /// Save Profile Address
        /// </summary>
        /// <param name="profileAddress"></param>
        private void SaveProfileAddress(ProfileAddress profileAddress)
        {
            IEntityValidator entityValidator = EntityValidatorFactory.CreateValidator();
            if (entityValidator.IsValid(profileAddress))
            {
                _profileAddressRepository.Add(profileAddress);
                _profileAddressRepository.UnitOfWork.Commit();

                return;
            }

            throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profileAddress));
        }

        /// <summary>
        /// Update profile address
        /// </summary>
        /// <param name="lstUpdatedAddressDTO"></param>
        /// <param name="lstCurrentAddress"></param>
        /// <param name="profile"></param>
        private void UpdateAddress(List<AddressDTO> lstUpdatedAddressDTO, List<ProfileAddress> lstCurrentAddress, Profile profile)
        {
            if (lstUpdatedAddressDTO == null && lstCurrentAddress == null)
                return;

            // if user has deleted all existing addresses
            if (lstUpdatedAddressDTO == null && lstCurrentAddress != null)
            {
                foreach (ProfileAddress address in lstCurrentAddress)
                {
                    DeleteProfileAddress(address);
                }
                return;
            }
            // if user has added new addresses and there was not any existing addresses
            if (lstUpdatedAddressDTO != null && lstCurrentAddress == null)
            {
                foreach (AddressDTO addressDTO in lstUpdatedAddressDTO)
                {
                    SaveAddress(addressDTO, profile);
                }
                return;
            }

            // if user has updated or deleted any record
            List<AddressDTO> lstNewAddress = lstUpdatedAddressDTO;

            // Check if address exists in database
            foreach (ProfileAddress profileAddress in lstCurrentAddress)
            {
                AddressDTO objAddressDTO = lstUpdatedAddressDTO.FirstOrDefault(x => x.AddressId == profileAddress.AddressId);
                if (objAddressDTO != null)
                {
                    Address updatedAddress = new Address
                    {
                        AddressId = objAddressDTO.AddressId,
                        AddressLine1 = objAddressDTO.AddressLine1,
                        AddressLine2 = objAddressDTO.AddressLine2,
                        City = objAddressDTO.City,
                        State = objAddressDTO.State,
                        Country = objAddressDTO.Country,
                        ZipCode = objAddressDTO.ZipCode
                    };

                    UpdateAddress(profileAddress.Address, updatedAddress);
                    lstNewAddress.Remove(objAddressDTO);
                }
                else
                {
                    DeleteProfileAddress(profileAddress);
                }
            }

            // Save new address
            foreach (AddressDTO addressDTO in lstNewAddress)
            {
                this.SaveAddress(addressDTO, profile);
            }
        }

        /// <summary>
        /// Update existing Address
        /// </summary>
        /// <param name="currentAddress"></param>
        /// <param name="updatedAddress"></param>
        private void UpdateAddress(Address currentAddress, Address updatedAddress)
        {
            updatedAddress.Created = currentAddress.Created;
            updatedAddress.CreatedBy = currentAddress.CreatedBy;
            updatedAddress.Updated = DateTime.Now;
            updatedAddress.UpdatedBy = "Updated User";

            IEntityValidator entityValidator = EntityValidatorFactory.CreateValidator();
            if (entityValidator.IsValid(updatedAddress))
            {
                _addressRepository.Merge(currentAddress, updatedAddress);
                _addressRepository.UnitOfWork.Commit();

                return;
            }

            throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedAddress));
        }

        /// <summary>
        /// Delete profile address
        /// </summary>
        /// <param name="profileAddress"></param>
        public void DeleteProfileAddress(ProfileAddress profileAddress)
        {
            var address = _addressRepository.Get(profileAddress.AddressId);

            if (address != null)
            {
                _profileAddressRepository.Remove(profileAddress);
                _profileAddressRepository.UnitOfWork.Commit();

                _addressRepository.Remove(address);
                _addressRepository.UnitOfWork.Commit();
            }
            else
            {
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingAddress);
            }
        }

        /// <summary>
        /// Add new phone
        /// </summary>
        /// <param name="phoneDTO"></param>
        /// <param name="profile"></param>
        private void SavePhone(PhoneDTO phoneDTO, Profile profile)
        {
            if (phoneDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            // Create a new Phone entity
            Phone phone = PhoneFactory.CreatePhone(phoneDTO.Number, "Anand", DateTime.Now, "Anand", DateTime.Now);
            phone = SavePhone(phone);

            PhoneType phoneType = _phoneTypeRepository.Get(phoneDTO.PhoneTypeId);

            // Create a new ProfilePhone entity
            ProfilePhone profilePhone = ProfilePhoneFactory.CreateProfilePhone(profile, phone, phoneType, "Anand", DateTime.Now, "Anand", DateTime.Now);
            SaveProfilePhone(profilePhone);
        }

        /// <summary>
        /// Save phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        private Phone SavePhone(Phone phone)
        {
            IEntityValidator entityValidator = EntityValidatorFactory.CreateValidator();
            if (entityValidator.IsValid(phone))
            {
                _phoneRepository.Add(phone);
                _phoneRepository.UnitOfWork.Commit();

                return phone;
            }

            throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(phone));
        }

        /// <summary>
        /// Save profilephone
        /// </summary>
        /// <param name="profilePhone"></param>
        private void SaveProfilePhone(ProfilePhone profilePhone)
        {
            IEntityValidator entityValidator = EntityValidatorFactory.CreateValidator();
            if (entityValidator.IsValid(profilePhone))
            {
                _profilePhoneRepository.Add(profilePhone);
                _profilePhoneRepository.UnitOfWork.Commit();

                return;
            }

            throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profilePhone));
        }

        /// <summary>
        /// Update profile phone
        /// </summary>
        /// <param name="lstUpdatedPhoneDTO"></param>
        /// <param name="lstCurrentPhone"></param>
        /// <param name="profile"></param>
        private void UpdatePhone(List<PhoneDTO> lstUpdatedPhoneDTO, List<ProfilePhone> lstCurrentPhone, Profile profile)
        {
            if (lstUpdatedPhoneDTO == null && lstCurrentPhone == null)
                return;

            // if user has deleted all existing phones
            if (lstUpdatedPhoneDTO == null && lstCurrentPhone != null)
            {
                foreach (ProfilePhone profilePhone in lstCurrentPhone)
                {
                    DeleteProfilePhone(profilePhone);
                }
                return;
            }

            // if user has added new phone and there was not any existing phone
            if (lstUpdatedPhoneDTO != null && lstCurrentPhone == null)
            {
                foreach (PhoneDTO phoneDTO in lstUpdatedPhoneDTO)
                {
                    SavePhone(phoneDTO, profile);
                }
                return;
            }

            // if user has updated or deleted any record
            List<PhoneDTO> lstNewPhone = lstUpdatedPhoneDTO;

            // check if phone exists in database
            foreach (ProfilePhone profilePhone in lstCurrentPhone)
            {
                PhoneDTO objPhoneDTO = lstUpdatedPhoneDTO.FirstOrDefault(x => x.PhoneId == profilePhone.PhoneId);
                if (objPhoneDTO != null)
                {
                    Phone updatedPhone = new Phone
                    {
                        PhoneId = objPhoneDTO.PhoneId,
                        Number = objPhoneDTO.Number
                    };
                    UpdatePhone(profilePhone.Phone, updatedPhone);
                }
                else
                {
                    DeleteProfilePhone(profilePhone);
                }
            }

            // Save new phones
            foreach (PhoneDTO phoneDTO in lstNewPhone)
            {
                SavePhone(phoneDTO, profile);
            }
        }

        /// <summary>
        /// Update existing phone
        /// </summary>
        /// <param name="currentPhone"></param>
        /// <param name="updatedPhone"></param>
        private void UpdatePhone(Phone currentPhone, Phone updatedPhone)
        {
            updatedPhone.Created = currentPhone.Created;
            updatedPhone.CreatedBy = currentPhone.CreatedBy;
            updatedPhone.Updated = DateTime.Now;
            updatedPhone.UpdatedBy = "Updated User";

            IEntityValidator entityValidator = EntityValidatorFactory.CreateValidator();
            if (entityValidator.IsValid(updatedPhone))
            {
                _phoneRepository.Merge(currentPhone, updatedPhone);
                _phoneRepository.UnitOfWork.Commit();

                return;
            }

            throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedPhone));
        }

        /// <summary>
        /// Delete profile phone
        /// </summary>
        /// <param name="profilePhone"></param>
        public void DeleteProfilePhone(ProfilePhone profilePhone)
        {
            Phone phone = _phoneRepository.Get(profilePhone.PhoneId);

            if (phone != null)
            {
                _profilePhoneRepository.Remove(profilePhone);
                _profilePhoneRepository.UnitOfWork.Commit();

                _phoneRepository.Remove(phone);
                _phoneRepository.UnitOfWork.Commit();
            }
            else
            {
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingPhone);
            }
        }

        /// <summary>
        /// Save profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private Profile SaveProfile(Profile profile)
        {
            IEntityValidator entityValidator = EntityValidatorFactory.CreateValidator();
            if (entityValidator.IsValid(profile))
            {
                _profileRepository.Add(profile);
                _profileRepository.UnitOfWork.Commit();

                return profile;
            }

            throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profile));
        }

        /// <summary>
        /// Update existing Profile
        /// </summary>
        /// <param name="currentProfile"></param>
        /// <param name="updatedProfile"></param>
        /// <returns></returns>
        private Profile UpdateProfile(Profile currentProfile, Profile updatedProfile)
        {
            updatedProfile.Created = currentProfile.Created;
            updatedProfile.CreatedBy = currentProfile.CreatedBy;
            updatedProfile.Updated = DateTime.Now;
            updatedProfile.UpdatedBy = "Updated User";

            IEntityValidator entityValidator = EntityValidatorFactory.CreateValidator();
            if (entityValidator.IsValid(updatedProfile))
            {
                _profileRepository.Merge(currentProfile, updatedProfile);
                _profileRepository.UnitOfWork.Commit();

                return updatedProfile;
            }

            throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedProfile));
        }
    }
}
