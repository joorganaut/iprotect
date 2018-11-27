using AXAMansard.Framework.DAO;
using AXAMansard.Framework.DTO;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreDataAccess.Common
{
    public class NPCoreDAO<T, idT> where T : IDataObject
    {
        IDatabase database;
        public NPCoreDAO(IDatabase database)
        {
            this.database = database;
        }
        public NPCoreDAO(string connection)
        {
            this.database = NPDatabaseManager.GetDatabaseByConnectionName(connection);
        }

        public async Task<T> SaveAsync(T obj)
        {
            await database.InsertAsync<T>(obj);
            return obj;
        }
        public async Task SaveAsync(T obj, bool nonStatic)
        {
            await database.InsertAsync<T>(obj);
        }
        public void Save(T obj, bool nonStatic = true)
        {
            database.InsertAsync<T>(obj);
            //return obj;
        }

        public async Task<T> Update(T obj, params string[] args)
        {
            foreach (string s in args)
            {
                await database.UpdateAsync<T>(obj, CreateGetterExpression(s));
            }
            return obj;
        }
        public async Task UpdateAsync(T obj, bool nonStatic, params string[] args)
        {
            foreach (string s in args)
            {
                await database.UpdateAsync<T>(obj, CreateGetterExpression(s));
            }
        }
        public async Task UpdateAsync(T obj, params string[] args)
        {
            await database.UpdateAsync(obj, args);
        }
        public async Task<T> Retrieve(idT id)
        {
            return await database.SingleByIdAsync<T>(id);
        }

        public async Task<T> Retrieve(long id)
        {
            return await database.SingleByIdAsync<T>(id);
        }
        public void Commit()
        {
           database.CompleteTransaction();
        }
        public async Task<T> RetrieveDataObjectByParameterAsync(string query, params object[] args)
        {
            Sql sql = new Sql(query, args);
            return await database.SingleAsync<T>(sql);
        }
        public async Task<List<T>> RetrieveDataObjectsByParametersAsync(string query, params object[] args)
        {
            Sql sql = new Sql(query, args);
            return await database.FetchAsync<T>(sql);
        }

        public async Task<List<T>> RetrieveDataObjectsByParametersAsync(string query, string sort, string direction, int page, int pageSize = 10, params object[] args)
        {
            Sql sql = new Sql(query);
            return await database.FetchAsync<T>(page, pageSize, sql);
        }
        private Expression<Func<T, object>> CreateGetterExpression(string propertyName)
        {
            var getter = typeof(T).GetProperty(propertyName).GetGetMethod(); //propertyInfo.GetGetMethod();

            Expression<Func<T, object>> expression = m => getter.Invoke(m, new object[] { });
            return expression;
        }
    }
    public class NPDatabaseManager
    {
        static Dictionary<string, IDatabase> DataBaseHashSet = new Dictionary<string, IDatabase>();
        public static IDatabase GetDatabaseByConnectionName(string connectionName)
        {
            IDatabase result = null;
            if (!DataBaseHashSet.TryGetValue(connectionName, out result))
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName];
                SqlConnection sql = new SqlConnection(connectionString.ConnectionString);
                result = new Database(sql);
                DataBaseHashSet.Add(connectionName, result);
            }
            return result;
        }
    }
}
