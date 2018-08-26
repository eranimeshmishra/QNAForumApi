
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;

namespace QNAForum.Core.Data
{
    public interface IGenericRepository<TEntity>
    {
        int Add(TEntity entity, IDbTransaction transaction = null);

        Task<int> AddAsync(TEntity entity, IDbTransaction transaction = null);

        int DeleteById(int indentifier, IDbTransaction transaction = null);

        Task<int> DeleteByIdAsync(int identifier, IDbTransaction transaction = null);

        TEntity FindById(int identifier);

        Task<TEntity> FindByIdAsync(int identifier);

        IList<TEntity> FindAll();

        IList<TEntity> FindAllPaged(int page, int pageSize, string whereClause = "",bool orderByDescending = false);

        Task<IList<TEntity>> FindAllPagedAsync(int page, int pageSize, string whereClause = "",
            bool orderByDescending = false);

        bool Update(TEntity entity, IDbTransaction transaction = null);

        Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction = null);

        int FindCount();

        Task<int> FindCountAsync();

    }
}