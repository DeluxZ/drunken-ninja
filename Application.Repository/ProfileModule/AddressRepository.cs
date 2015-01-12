using Application.Core.ProfileModule.AddressAggregate;
using Application.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.ProfileModule
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public AddressRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
