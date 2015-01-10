using Application.Core.ProfileModule.AddressAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.EntityConfiguration
{
    public class AddressTypeConfiguration :EntityTypeConfiguration<AddressType>
    {
        public AddressTypeConfiguration()
        {
            this.HasKey(at => at.AddressTypeId);
            this.Property(at => at.Name).HasMaxLength(50).IsRequired();

            // Configure table map
            this.ToTable("AddressType");
        }
    }
}
