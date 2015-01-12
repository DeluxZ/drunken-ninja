using Application.Core.ProfileModule.ProfileAddressAggregate;
using Application.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.ProfileModule
{
    public class ProfileAddressRepository:Repository<ProfileAddress>, IProfileAddressRepository
    {
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProfileAddressRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
