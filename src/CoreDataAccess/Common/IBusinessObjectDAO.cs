using AXAMansard.Framework.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreDataAccess
{
    public interface IBusinessObjectDAO
    { }
    public interface INHBusinessObjectDAO<T>: IBusinessObjectDAO where T : class, IDataObject
    {
        Task SaveAsync(T obj, bool nonStatic = true);
        Task UpdateAsync(T obj, bool nonStatic = true, bool overLoad = true);
        void Save(T obj, bool nonStatic = true);
        Task CommitChangesAsync();
        void CommitChanges();

        Task<T> RetrieveAsync(long id);
        Task<T> RetrieveDataObjectByParameterAsync(KeyValuePair<string, object>[] parameters);
        Task<IList<T>> RetrieveDataObjectsByParametersAsync(KeyValuePair<string, object>[] parameters);
        Task<IList<T>> RetrieveDataObjectsByParametersAsync(KeyValuePair<string, object>[] parameters, string sort, string direction, int page, int pageSize);
    }

    public interface INPBusinessObjectDAO<T>: IBusinessObjectDAO where T : class, IDataObject
    {
        Task SaveAsync(T obj, bool nonStatic = true);
        Task UpdateAsync(T obj, params string[] args);
        void Save(T obj, bool nonStatic = true);
        void Commit();
        Task<T> Retrieve(long id);
        Task<T> RetrieveDataObjectByParameterAsync(string query, params object[] args);
        Task<List<T>> RetrieveDataObjectsByParametersAsync(string query, params object[] args);
        Task<List<T>> RetrieveDataObjectsByParametersAsync(string query, string sort, string direction, int page, int pageSize = 10, params object[] args);
    }
}
