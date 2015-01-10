using Application.Core.ProfileModule.PhoneAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.EntityConfiguration
{
    public class PhoneConfiguration : EntityTypeConfiguration<Phone>
    {
        public PhoneConfiguration()
        {
            this.HasKey(p => p.PhoneId);
            this.Property(p => p.Number).HasMaxLength(25).IsRequired();

            // Configure table map
            this.ToTable("Phone");
        }
    }
}
