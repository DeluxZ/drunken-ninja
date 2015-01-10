using Application.Core.ProfileModule.PhoneAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.EntityConfiguration
{
    public class PhoneTypeConfiguration : EntityTypeConfiguration<PhoneType>
    {
        public PhoneTypeConfiguration()
        {
            this.HasKey(pt => pt.PhoneTypeId);
            this.Property(pt => pt.Name).HasMaxLength(50).IsRequired();

            // Configure table map
            this.ToTable("PhoneType");
        }
            
    }
}
