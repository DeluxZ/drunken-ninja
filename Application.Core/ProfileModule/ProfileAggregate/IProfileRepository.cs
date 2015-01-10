using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.ProfileModule.ProfileAggregate
{
    /// <summary>
    /// Base contract for Profile repository
    /// </summary>
    public interface IProfileRepository : IRepository<Profile>
    {
    }
}
