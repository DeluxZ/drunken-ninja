using Application.Core.ProfileModule.ProfileAddressAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.EntityConfiguration
{
    public class ProfileAddressConfiguration :EntityTypeConfiguration<ProfileAddress>
    {
        public ProfileAddressConfiguration()
        {
            this.HasKey(pa => pa.ProfileAddressId);
            // 1 .. *
            this.HasRequired(pa => pa.Address)
                .WithMany(pa => pa.ProfileAddresses)
                .HasForeignKey(pa => pa.AddressId)
                .WillCascadeOnDelete(false);
            // 1 .. *
            this.HasRequired(pa => pa.AddressType)
                .WithMany(pa => pa.ProfileAddresses)
                .HasForeignKey(pa => pa.AddressTypeId)
                .WillCascadeOnDelete(false);
            // 1 .. *
            this.HasRequired(pa => pa.Profile)
                .WithMany(pa => pa.ProfileAddresses)
                .HasForeignKey(pa => pa.ProfileId)
                .WillCascadeOnDelete(false);

            // Configure table map
            this.ToTable("ProfileAddress");
        }
    }
}
