using System;
using System.Data;
using System.Threading.Tasks;

namespace QNAForum.Core.Data
{
    public interface IConnectionFactory
    {
        T WithConnection<T>(Func<IDbConnection, T> getData);
        Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> getData);
    }
}