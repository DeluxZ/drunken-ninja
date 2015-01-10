using Application.Core.ProfileModule.ProfilePhoneAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.EntityConfiguration
{
    public class ProfilePhoneConfiguration :EntityTypeConfiguration<ProfilePhone>
    {
        public ProfilePhoneConfiguration()
        {
            this.HasKey(pp => pp.ProfilePhoneId);
            // 1 .. *
            this.HasRequired(pp => pp.Phone)
                .WithMany(pp => pp.ProfilePhones)
                .HasForeignKey(pp => pp.PhoneId)
                .WillCascadeOnDelete(false);
            // 1 .. *
            this.HasRequired(pp => pp.PhoneType)
                .WithMany(pp => pp.ProfilePhones)
                .HasForeignKey(pp => pp.PhoneTypeId)
                .WillCascadeOnDelete(false);
            // 1 .. *
            this.HasRequired(pp => pp.Profile)
                .WithMany(pp => pp.ProfilePhones)
                .HasForeignKey(pp => pp.ProfileId)
                .WillCascadeOnDelete(false);

            // Configure table map
            this.ToTable("ProfilePhone");
        }
    }
}
