using Application.Core.ProfileModule.ProfilePhoneAggregate;
using Application.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.ProfileModule
{
    public class ProfilePhoneRepository : Repository<ProfilePhone>, IProfilePhoneRepository
    {
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProfilePhoneRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
