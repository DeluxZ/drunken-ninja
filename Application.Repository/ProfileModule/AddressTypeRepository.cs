using Application.Core.ProfileModule.AddressAggregate;
using Application.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.ProfileModule
{
    public class AddressTypeRepository : Repository<AddressType>, IAddressTypeRepository
    {
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public AddressTypeRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
