using Application.Core.ProfileModule.PhoneAggregate;
using Application.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.ProfileModule
{
    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public PhoneRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
