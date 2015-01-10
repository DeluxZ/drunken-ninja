using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.AddressAggregate
{
    /// <summary>
    /// Base contract for Address repository
    /// </summary>
    public interface IAddressRepository : IRepository<Address>
    {
    }
}
