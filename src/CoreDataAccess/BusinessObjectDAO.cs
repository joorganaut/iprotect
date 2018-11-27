using AXAMansard.Framework.DAO;
using AXAMansard.Framework.DTO;
using CoreDataAccess.Common;
using NHibernate;
using NHibernate.Criterion;
using NPoco;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDataAccess
{
    public class NHBusinessObjectDAO<T> : CoreDAO<T, long>, INHBusinessObjectDAO<T> where T : class, IDataObject
    {

        public int ItemsCount = 0;
        public NHBusinessObjectDAO() : base()
        {
        }
        public T RetrieveDataObjectByParameter(KeyValuePair<string, object> parameter)
        {
            T result = default(T);//default(IDataObject);
            ISession session = BuildSession("");
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                if (parameter.Value != null)
                {
                    criteria.Add(Expression.Eq(parameter.Key, parameter.Value));
                }
                result = criteria.UniqueResult<T>();
            }
            catch { throw; }

            return result;
        }
        public async Task<T> RetrieveDataObjectByParameterAsync(KeyValuePair<string, object> parameter)
        {
            T result = default(T);//default(IDataObject);
            ISession session = BuildSession("");
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                if (parameter.Value != null)
                {
                    criteria.Add(Expression.Eq(parameter.Key, parameter.Value));
                }
                result = await criteria.UniqueResultAsync<T>();
            }
            catch { throw; }

            return result;
        }
        public T RetrieveDataObjectByParameter(KeyValuePair<string, object>[] parameters)
        {
            T result = default(T);//default(IDataObject);
            ISession session = BuildSession("");
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                for (int i = 0; i < parameters.Length; i++)
                {
                    string propertyName = parameters[i].Key;
                    object propertyValue = parameters[i].Value;
                    if (propertyValue != null)
                    {
                        if (propertyValue.GetType() == typeof(string))
                        {
                            if (!string.IsNullOrWhiteSpace(propertyValue.ToString()))
                            {
                                criteria.Add(Expression.Like(propertyName, propertyValue.ToString(), MatchMode.Anywhere));
                            }
                        }
                        else if (propertyValue.GetType() == typeof(DateTime) && !((DateTime)propertyValue).Equals(DateTime.MinValue))
                        {
                            if (propertyName.ToLower().Contains("from"))
                            {
                                criteria.Add(Expression.Ge(propertyName, propertyValue));
                            }
                            else
                            {
                                criteria.Add(Expression.Le(propertyName, propertyValue));
                            }
                        }
                        else
                        {
                            criteria.Add(Expression.Eq(propertyName, propertyValue));
                        }
                    }
                }
                result = criteria.UniqueResult<T>();
            }
            catch { throw; }

            return result;
        }

        public async Task<T> RetrieveDataObjectByParameterAsync(KeyValuePair<string, object>[] parameters)
        {
            T result = default(T);//default(IDataObject);
            ISession session = BuildSession("");
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                for (int i = 0; i < parameters.Length; i++)
                {
                    string propertyName = parameters[i].Key;
                    object propertyValue = parameters[i].Value;
                    if (propertyValue != null)
                    {
                        if (propertyValue.GetType() == typeof(string))
                        {
                            if (!string.IsNullOrWhiteSpace(propertyValue.ToString()))
                            {
                                criteria.Add(Expression.Like(propertyName, propertyValue.ToString(), MatchMode.Anywhere));
                            }
                        }
                        else if (propertyValue.GetType() == typeof(DateTime) && !((DateTime)propertyValue).Equals(DateTime.MinValue))
                        {
                            if (propertyName.ToLower().Contains("from"))
                            {
                                criteria.Add(Expression.Ge(propertyName, propertyValue));
                            }
                            else
                            {
                                criteria.Add(Expression.Le(propertyName, propertyValue));
                            }
                        }
                        else
                        {
                            criteria.Add(Expression.Eq(propertyName, propertyValue));
                        }
                    }
                }
                result = await criteria.UniqueResultAsync<T>();
            }
            catch { throw; }

            return result;
        }
        public IList<T> RetrieveDataObjectsByParameters(KeyValuePair<string, object>[] parameters)
        {
            IList<T> result = null;
            ISession session = BuildSession("");
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));

                for (int i = 0; i < parameters.Length; i++)
                {
                    string propertyName = parameters[i].Key;
                    object propertyValue = parameters[i].Value;
                    if (propertyValue != null)
                    {
                        if (propertyValue.GetType() == typeof(string))
                        {
                            if (!string.IsNullOrWhiteSpace(propertyValue.ToString()))
                            {
                                criteria.Add(Expression.Like(propertyName, propertyValue.ToString(), MatchMode.Anywhere));
                            }
                        }
                        else if (propertyValue.GetType() == typeof(DateTime) && !((DateTime)propertyValue).Equals(DateTime.MinValue))
                        {
                            if (propertyName.ToLower().Contains("from"))
                            {
                                criteria.Add(Expression.Ge(propertyName, propertyValue));
                            }
                            else
                            {
                                criteria.Add(Expression.Le(propertyName, propertyValue));
                            }
                        }
                        else
                        {
                            criteria.Add(Expression.Eq(propertyName, propertyValue));
                        }
                    }
                }

                //criteria.Add(Expression.Eq("IsTreated", false));
                ICriteria listCriteria = CriteriaTransformer.Clone(criteria);
                //This section then performs the sort operations on the list. Sorting defaults to the Name column

                //Add the two criteria to the session and retrieve their result.
                IList<T> allResults = listCriteria.List<T>();//session.CreateMultiCriteria().Add(listCriteria).Add(countCriteria).List();
                foreach (var o in allResults)
                {
                    result.Add((T)o);
                }
            }
            catch (Exception e)
            {
                //errMsg = e.Message;
            }
            return result;
        }

        public IList<T> RetrieveDataObjectsByParameters(KeyValuePair<string, object>[] parameters, string sort, string direction, int page, int pageSize)
        {
            IList<T> result = new List<T>();
            ItemsCount = -1;
            ISession session = BuildSession("");
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));

                for (int i = 0; i < parameters.Length; i++)
                {
                    string propertyName = parameters[i].Key;
                    object propertyValue = parameters[i].Value;
                    if (propertyValue != null)
                    {
                        if (propertyValue.GetType() == typeof(string))
                        {
                            if (!string.IsNullOrWhiteSpace(propertyValue.ToString()))
                            {
                                criteria.Add(Expression.Like(propertyName, propertyValue.ToString(), MatchMode.Anywhere));
                            }
                        }
                        else if (propertyValue.GetType() == typeof(DateTime) && !((DateTime)propertyValue).Equals(DateTime.MinValue))
                        {
                            if (propertyName.ToLower().Contains("from"))
                            {
                                criteria.Add(Expression.Ge(propertyName, propertyValue));
                            }
                            else
                            {
                                criteria.Add(Expression.Le(propertyName, propertyValue));
                            }
                        }
                        else
                        {
                            criteria.Add(Expression.Eq(propertyName, propertyValue));
                        }
                    }
                }

                //criteria.Add(Expression.Eq("IsTreated", false));
                ICriteria countCriteria = CriteriaTransformer.Clone(criteria).SetProjection(Projections.RowCountInt64());
                ICriteria listCriteria = CriteriaTransformer.Clone(criteria).SetFirstResult(page * pageSize).SetMaxResults(pageSize);
                //This section then performs the sort operations on the list. Sorting defaults to the Name column
                if (direction == "Default" || string.IsNullOrEmpty(direction))
                {
                    listCriteria.AddOrder(Order.Desc("ID"));
                }
                else
                {
                    if (direction == "DESC")
                    {
                        listCriteria.AddOrder(Order.Desc(sort));
                    }
                    else
                    {
                        listCriteria.AddOrder(Order.Asc(sort));
                    }
                }
                //Add the two criteria to the session and retrieve their result.
                IList allResults = listCriteria.List();//session.CreateMultiCriteria().Add(listCriteria).Add(countCriteria).List();
                IList allCount = countCriteria.List();
                foreach (var o in allResults)
                {
                    result.Add((T)o);
                }

                ItemsCount = allCount.Count <= 0 ? 0 : Convert.ToInt32((long)((IList)allCount)[0]);
            }
            catch (Exception e)
            {
                //errMsg = e.Message;
            }
            return result;
        }


        public async Task<IList<T>> RetrieveDataObjectsByParametersAsync(KeyValuePair<string, object>[] parameters, string sort, string direction, int page, int pageSize)
        {
            IList<T> result = new List<T>();
            ItemsCount = -1;
            ISession session = BuildSession("");
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));

                for (int i = 0; i < parameters.Length; i++)
                {
                    string propertyName = parameters[i].Key;
                    object propertyValue = parameters[i].Value;
                    if (propertyValue != null)
                    {
                        if (propertyValue.GetType() == typeof(string))
                        {
                            if (!string.IsNullOrWhiteSpace(propertyValue.ToString()))
                            {
                                criteria.Add(Expression.Like(propertyName, propertyValue.ToString(), MatchMode.Anywhere));
                            }
                        }
                        else if (propertyValue.GetType() == typeof(DateTime) && !((DateTime)propertyValue).Equals(DateTime.MinValue))
                        {
                            if (propertyName.ToLower().Contains("from"))
                            {
                                criteria.Add(Expression.Ge(propertyName, propertyValue));
                            }
                            else
                            {
                                criteria.Add(Expression.Le(propertyName, propertyValue));
                            }
                        }
                        else
                        {
                            criteria.Add(Expression.Eq(propertyName, propertyValue));
                        }
                    }
                }

                //criteria.Add(Expression.Eq("IsTreated", false));
                ICriteria countCriteria = CriteriaTransformer.Clone(criteria).SetProjection(Projections.RowCountInt64());
                ICriteria listCriteria = CriteriaTransformer.Clone(criteria).SetFirstResult(page * pageSize).SetMaxResults(pageSize);
                //This section then performs the sort operations on the list. Sorting defaults to the Name column
                if (direction == "Default" || string.IsNullOrEmpty(direction))
                {
                    listCriteria.AddOrder(Order.Desc("ID"));
                }
                else
                {
                    if (direction == "DESC")
                    {
                        listCriteria.AddOrder(Order.Desc(sort));
                    }
                    else
                    {
                        listCriteria.AddOrder(Order.Asc(sort));
                    }
                }
                //Add the two criteria to the session and retrieve their result.
                IList allResults = await listCriteria.ListAsync();//session.CreateMultiCriteria().Add(listCriteria).Add(countCriteria).List();
                IList allCount = countCriteria.List();
                foreach (var o in allResults)
                {
                    result.Add((T)o);
                }

                ItemsCount = allCount.Count <= 0 ? 0 : Convert.ToInt32((long)((IList)allCount)[0]);
            }
            catch (Exception e)
            {
                //errMsg = e.Message;
            }
            return result;
        }

        public async Task<IList<T>> RetrieveDataObjectsByParametersAsync(KeyValuePair<string, object>[] parameters)
        {
            IList<T> result = null;
            ISession session = BuildSession("");
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));

                for (int i = 0; i < parameters.Length; i++)
                {
                    string propertyName = parameters[i].Key;
                    object propertyValue = parameters[i].Value;
                    if (propertyValue != null)
                    {
                        if (propertyValue.GetType() == typeof(string))
                        {
                            if (!string.IsNullOrWhiteSpace(propertyValue.ToString()))
                            {
                                criteria.Add(Expression.Like(propertyName, propertyValue.ToString(), MatchMode.Anywhere));
                            }
                        }
                        else if (propertyValue.GetType() == typeof(DateTime) && !((DateTime)propertyValue).Equals(DateTime.MinValue))
                        {
                            if (propertyName.ToLower().Contains("from"))
                            {
                                criteria.Add(Expression.Ge(propertyName, propertyValue));
                            }
                            else
                            {
                                criteria.Add(Expression.Le(propertyName, propertyValue));
                            }
                        }
                        else
                        {
                            criteria.Add(Expression.Eq(propertyName, propertyValue));
                        }
                    }
                }

                //criteria.Add(Expression.Eq("IsTreated", false));
                ICriteria listCriteria = CriteriaTransformer.Clone(criteria);
                //This section then performs the sort operations on the list. Sorting defaults to the Name column

                //Add the two criteria to the session and retrieve their result.
                IList<T> allResults = await listCriteria.ListAsync<T>();//session.CreateMultiCriteria().Add(listCriteria).Add(countCriteria).List();
                foreach (var o in allResults)
                {
                    result.Add((T)o);
                }
            }
            catch (Exception e)
            {
                //errMsg = e.Message;
            }
            return result;
        }

        
    }

    public class NPBusinessObjectDAO<T> : NPCoreDAO<T, long>, INPBusinessObjectDAO<T> where T : class, IDataObject
    {
        IDatabase database;
        public NPBusinessObjectDAO(IDatabase database) : base(database)
        {
            this.database = database;
        }
        public NPBusinessObjectDAO(string connect) : base(connect)
        {
        }
    }
}
