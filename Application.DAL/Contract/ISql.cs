using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Contract
{
    /// <summary>
    /// Base contract for support 'dialect specific queries'
    /// </summary>
    public interface ISql
    {
        /// <summary>
        /// Execute specific query with underlying persistance store
        /// </summary>
        /// <typeparam name="T">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">Dialect query</param>
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>Enumerable results</returns>
        IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters);

        /// <summary>
        /// Execute arbitrary command into underlying persistance store
        /// </summary>
        /// <param name="sqlCommand">Command to execute</param>
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}
