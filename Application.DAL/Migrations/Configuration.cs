namespace Application.DAL.Migrations
{
    using Application.Core.ProfileModule.AddressAggregate;
    using Application.Core.ProfileModule.PhoneAggregate;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Application.DAL.UnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Application.DAL.UnitOfWork context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.AddressType.AddOrUpdate(
                p => p.AddressTypeId,
                new AddressType { Name = "Billing Address" },
                new AddressType { Name = "Shipping Address" }
            );

            context.PhoneType.AddOrUpdate(
                p => p.PhoneTypeId,
                new PhoneType { Name = "Home Phone" },
                new PhoneType { Name = "Work Phone" }
            );
        }
    }
}
