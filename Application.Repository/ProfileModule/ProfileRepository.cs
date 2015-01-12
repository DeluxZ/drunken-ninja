using Application.Core.ProfileModule.ProfileAggregate;
using Application.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.ProfileModule
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProfileRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
