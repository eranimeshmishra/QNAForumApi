using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;


namespace QNAForum.Core.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel, new()
    {
        private IConnectionFactory _connectionFactory;

        public GenericRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            Type type = typeof(TEntity);

            TableName = Extension.GetTableName(type);
            PrimaryKey = Extension.GetSingleKey(type).Name;
        }

        #region Protected Properties

        protected string TableName { get; private set; }

        protected string PrimaryKey { get; private set; }


        #endregion
     

        public int Add(TEntity entity, IDbTransaction transaction = null)
        {
            return _connectionFactory.WithConnection(connection => (int)connection.Insert(entity, transaction)

            );
        }

        public async Task<int> AddAsync(TEntity entity, IDbTransaction transaction = null)
        {
            return await _connectionFactory.WithConnectionAsync(async connection =>
                await connection.InsertAsync(entity, transaction));
        }

        public int DeleteById(int identifier, IDbTransaction transaction = null)
        {
            string sql = string.Format("delete from {0} where {1}=@TableId", TableName, PrimaryKey);
            return _connectionFactory.WithConnection(connection => connection.Execute(sql, new { TableId = identifier }));

        }

        public async Task<int> DeleteByIdAsync(int identifier, IDbTransaction transaction = null)
        {
            string sql = string.Format("delete from {0} where {1}=@TableId", TableName, PrimaryKey);
            return await _connectionFactory.WithConnectionAsync(async connection => await connection.ExecuteAsync(sql, new { TableId = identifier }));

        }

        public IList<TEntity> FindAll()
        {
            return _connectionFactory.WithConnection(connection => connection.GetAll<TEntity>().ToList()
            );
        }

        public async Task<IList<TEntity>> FindAllAsync()
        {
            IEnumerable<TEntity > result = await _connectionFactory.WithConnection(async connection => await connection.GetAllAsync<TEntity>()
            );
            return result.ToList();
        }

        public IList<TEntity> FindAllPaged(int page, int pageSize, string whereClause = "", bool orderByDescending = false)
        {
            throw new System.NotImplementedException();
        }

        public async  Task<IList<TEntity>> FindAllPagedAsync(int page, int pageSize, string whereClause = "", bool orderByDescending = false)
        {
            string sql = string.Format( "select * from {0} @whereClause",TableName);
            IEnumerable<TEntity> result = await _connectionFactory.WithConnection(async connection => await connection.QueryAsync<TEntity>(sql,new{ whereClause })
            );
            return result.ToList();
        }

        public TEntity FindById(int identifier)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> FindByIdAsync(int identifier)
        {
            throw new System.NotImplementedException();
        }

        public int FindCount()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> FindCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public bool Update(TEntity entity, IDbTransaction transaction = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction = null)
        {
            throw new System.NotImplementedException();
        }
    }
}

namespace Dapper
{
    public static class Configuration
    {

        /// <summary>
        ///  Enable this to use the type name as table name. This will override the
        ///  default <see cref="TableAttribute"/> behavior
        /// </summary>
        public static bool UseTypeNameAsTableName
        {
            set
            {
                if (value)
                {
                    string schema = "dbo";
                    if (!string.IsNullOrWhiteSpace(DBSchema)) schema = DBSchema;

                    SqlMapperExtensions.TableNameMapper = type => string.Format("[{0}].[{1}]", schema, type.Name);
                }
            }
        }

        public static string DBSchema { get; set; }
    }

    /// <summary>
    ///  All code is taken directly from Dapper.Contrib to extend 
    ///  the list of extension methods available with Dapper and Dapper.Contrib
    ///  refer: https://github.com/StackExchange/Dapper/blob/master/Dapper.Contrib/SqlMapperExtensions.cs
    /// </summary>
    public static class Extension
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName = new ConcurrentDictionary<RuntimeTypeHandle, string>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ExplicitKeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();

        public static string GetTableName(Type type)
        {
            string name;

            // try to find the table name in exisiting dictionary
            if (TypeTableName.TryGetValue(type.TypeHandle, out name)) return name;

            if (SqlMapperExtensions.TableNameMapper != null)
            {
                name = SqlMapperExtensions.TableNameMapper(type);
            }
            else
            {
                //NOTE: This as dynamic trick should be able to handle both our own Table-attribute as well as the one in EntityFramework 
                var tableAttr = type
#if COREFX
                    .GetTypeInfo()
#endif
                    .GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;
                if (tableAttr != null)
                    name = tableAttr.Name;
                else
                {
                    name = type.Name + "s";
                    if (type.IsInterface && name.StartsWith("I"))
                        name = name.Substring(1);
                }
            }

            // add to the table name dictionary
            TypeTableName[type.TypeHandle] = name;
            return name;
        }

        public static PropertyInfo GetSingleKey(Type type)
        {
            var keys = KeyPropertiesCache(type);
            var explicitKeys = ExplicitKeyPropertiesCache(type);
            var keyCount = keys.Count + explicitKeys.Count;
            if (keyCount > 1)
                throw new DataException("Dapper only supports an entity with a single [Key] or [ExplicitKey] property");
            if (keyCount == 0)
                throw new DataException("Dapper only supports an entity with a [Key] or an [ExplicitKey] property");

            return keys.Any() ? keys.First() : explicitKeys.First();
        }

        private static List<PropertyInfo> KeyPropertiesCache(Type type)
        {
            IEnumerable<PropertyInfo> pi;
            if (KeyProperties.TryGetValue(type.TypeHandle, out pi))
            {
                return pi.ToList();
            }

            var allProperties = TypePropertiesCache(type);
            var keyProperties = allProperties.Where(p =>
            {
                return p.GetCustomAttributes(true).Any(a => a is KeyAttribute);
            }).ToList();

            if (keyProperties.Count == 0)
            {
                var idProp = allProperties.FirstOrDefault(p => p.Name.ToLower() == "id");
                if (idProp != null && !idProp.GetCustomAttributes(true).Any(a => a is ExplicitKeyAttribute))
                {
                    keyProperties.Add(idProp);
                }
            }

            KeyProperties[type.TypeHandle] = keyProperties;
            return keyProperties;
        }

        private static List<PropertyInfo> ExplicitKeyPropertiesCache(Type type)
        {
            IEnumerable<PropertyInfo> pi;
            if (ExplicitKeyProperties.TryGetValue(type.TypeHandle, out pi))
            {
                return pi.ToList();
            }

            var explicitKeyProperties = TypePropertiesCache(type).Where(p => p.GetCustomAttributes(true).Any(a => a is ExplicitKeyAttribute)).ToList();

            ExplicitKeyProperties[type.TypeHandle] = explicitKeyProperties;
            return explicitKeyProperties;
        }

        private static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            IEnumerable<PropertyInfo> pis;
            if (TypeProperties.TryGetValue(type.TypeHandle, out pis))
            {
                return pis.ToList();
            }

            var properties = type.GetProperties().Where(IsWriteable).ToArray();
            TypeProperties[type.TypeHandle] = properties;
            return properties.ToList();
        }

        private static bool IsWriteable(PropertyInfo pi)
        {
            var attributes = pi.GetCustomAttributes(typeof(WriteAttribute), false).AsList();
            if (attributes.Count != 1) return true;

            var writeAttribute = (WriteAttribute)attributes[0];
            return writeAttribute.Write;
        }
    }
}