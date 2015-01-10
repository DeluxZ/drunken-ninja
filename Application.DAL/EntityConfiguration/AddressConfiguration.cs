using Application.Core.ProfileModule.AddressAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.EntityConfiguration
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            this.HasKey(a => a.AddressId);
            this.Property(a => a.AddressLine1).HasMaxLength(100).IsRequired();
            this.Property(a => a.AddressLine2).HasMaxLength(100).IsRequired();
            this.Property(a => a.Country).HasMaxLength(50).IsRequired();
            this.Property(a => a.State).HasMaxLength(50).IsRequired();
            this.Property(a => a.City).HasMaxLength(50).IsRequired();
            this.Property(a => a.ZipCode).HasMaxLength(15).IsRequired();

            // configure table map
            this.ToTable("Address");
        }
    }
}
