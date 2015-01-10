using Application.Core.ProfileModule.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.EntityConfiguration
{
    public class ProfileConfiguration :EntityTypeConfiguration<Profile>
    {
        public ProfileConfiguration()
        {
            this.HasKey(p => p.ProfileId);
            this.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
            this.Property(p => p.LastName).HasMaxLength(50).IsRequired();
            this.Property(p => p.Email).HasMaxLength(50).IsRequired();

            // Configure table map
            this.ToTable("Profile");
        }
    }
}
