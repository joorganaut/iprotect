using AXAMansard.Framework.DTO;
using CoreBusinessLogic.Common;
using CoreDataAccess;
using Ninject;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CoreBusinessLogic.Common.Unity
{
    public class DataAccessDIConfiguration<T> : UnityConfiguration<T> where T : class, IBusinessObjectDAO
    {
        //IUnityContainer Container { get; set;S }
        public DataAccessDIConfiguration():base()
        {
            RegisterInterfaces();
        }
        internal override void RegisterInterfaces()
        {
            Container.RegisterType<INHBusinessObjectDAO<IDataObject>, NHBusinessObjectDAO<IDataObject>>();
            Container.RegisterType<INPBusinessObjectDAO<IDataObject>, NPBusinessObjectDAO<IDataObject>>();
        }
    }
    public enum DataImplementationType
    {
        NH = 0,
        NP = 1,
        RD = 2
    }
    public class Save<T> : IService<T> where T : class, IDataObject
    {
        public Save()
        {
        }
        public string Error { get; set; }
        IUnityContainer Container;
        public DataImplementationType DataImplementationType { get; set; }
        
        public object ExecuteAction(object obj, out string errMsg)
        {
            bool result = false;
            errMsg = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    BusinessObjectDAO.Save((T)obj);
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();
                    //DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                                                                                                                         // var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    BusinessObjectDAO.Save((T)obj);
                }
                result = true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }
        public async Task<object> ExecuteActionAsync(object obj)
        {
            bool result = false;
            Error = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();
                    //DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.SaveAsync((T)obj);
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();
                    //DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.SaveAsync((T)obj);
                }
                result = true;
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }
        public object ExecuteAction(out string errMsg, KeyValuePair<string, object>[] parameters, bool withPaging = true)
        {
            bool result = false;
            errMsg = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();
                   // DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    BusinessObjectDAO.Save((T)parameters.GetValue(0));
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();
                    //DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    BusinessObjectDAO.Save((T)parameters.GetValue(0));
                }
                result = true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }

        public async Task<object> ExecuteActionAsync(KeyValuePair<string, object>[] parameters, bool withPaging = true)
        {
            bool result = false;
            Error = string.Empty;
            try
            {
               // var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.SaveAsync((T)parameters.GetValue(0));
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.SaveAsync((T)parameters.GetValue(0));
                }
                result = true;
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }

    }


    public class Commit<T> : IService<T> where T : class, IDataObject
    {
        public string Error { get; set; }
        IUnityContainer Container;
        public Commit()
        {
        }
        public DataImplementationType DataImplementationType { get; set; }
        public object ExecuteAction(object obj, out string errMsg)
        {
            bool result = false;
            errMsg = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    BusinessObjectDAO.Commit();
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    BusinessObjectDAO.CommitChangesAsync();
                }
                result = true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }

        public object ExecuteAction(out string errMsg, KeyValuePair<string, object>[] parameters, bool withPaging = true)
        {
            bool result = false;
            errMsg = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();//kernel.Get<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    BusinessObjectDAO.Commit();
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    BusinessObjectDAO.CommitChangesAsync();
                }
                result = true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }

        public async Task<object> ExecuteActionAsync(object obj)
        {
            bool result = false;
            Error = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.CommitChangesAsync();
                }
                result = true;
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }

        public async Task<object> ExecuteActionAsync(KeyValuePair<string, object>[] parameters, bool withPaging = true)
        {
            bool result = false;
            Error = string.Empty;
            try
            {

                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.CommitChangesAsync();
                }
                result = true;
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }
    }
    public class Update<T> : IService<T> where T : class, IDataObject
    {
        public string Error { get; set; }
        IUnityContainer Container;
        
        public DataImplementationType DataImplementationType { get; set; }
        public string[] args { get; set; }

        public object ExecuteAction(object obj, out string errMsg)
        {
            bool result = false;
            errMsg = string.Empty;
            try
            {

                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    BusinessObjectDAO.UpdateAsync((T)obj, args);
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    BusinessObjectDAO.UpdateAsync((T)obj);
                }
                result = true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }

        public object ExecuteAction(out string errMsg, KeyValuePair<string, object>[] parameters, bool withPaging = true)
        {
            bool result = false;
            errMsg = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    BusinessObjectDAO.UpdateAsync((T)parameters.GetValue(0), args);
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    BusinessObjectDAO.UpdateAsync((T)parameters.GetValue(0));
                }
                result = true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }

        public async Task<object> ExecuteActionAsync(object obj)
        {
            bool result = false;
            Error = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.UpdateAsync((T)obj, args);
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.UpdateAsync((T)obj);
                }
                result = true;
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }

        public async Task<object> ExecuteActionAsync(KeyValuePair<string, object>[] parameters, bool withPaging = true)
        {
            bool result = false;
            Error = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.UpdateAsync((T)parameters.GetValue(0), args);
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    await BusinessObjectDAO.UpdateAsync((T)parameters.GetValue(0));
                }
                result = true;
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }
    }
    public class Retrieve<T> : IService<T> where T : class, IDataObject
    {
        public string Error { get; set; }
        IUnityContainer Container;
       
        public string[] args { get; set; }
        string Query { get; set; }
        public DataImplementationType DataImplementationType { get; set; }
        public object ExecuteAction(object obj, out string errMsg)
        {
            T result = default(T);
            errMsg = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    result = BusinessObjectDAO.Retrieve(Convert.ToInt64(obj)).Result;
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    result = BusinessObjectDAO.RetrieveAsync(Convert.ToInt64(obj)).Result;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }

        public object ExecuteAction(out string errMsg, KeyValuePair<string, object>[] parameters, bool withPaging = true)
        {
            T result = default(T);
            errMsg = string.Empty;
            try
            {

                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    result = BusinessObjectDAO.RetrieveDataObjectByParameterAsync(Query, parameters.ToDynamicObject()).Result;
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    result = BusinessObjectDAO.RetrieveDataObjectByParameterAsync(parameters).Result;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }

        public async Task<object> ExecuteActionAsync(object obj)
        {
            T result = default(T);
            Error = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    result = await BusinessObjectDAO.Retrieve(Convert.ToInt64(obj));
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    result = await BusinessObjectDAO.RetrieveAsync(Convert.ToInt64(obj));
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }

        public async Task<object> ExecuteActionAsync(KeyValuePair<string, object>[] parameters, bool withPaging = true)
        {
            T result = default(T);
            Error = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    result = await BusinessObjectDAO.RetrieveDataObjectByParameterAsync(Query, parameters.ToDynamicObject());
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    result = await BusinessObjectDAO.RetrieveDataObjectByParameterAsync(parameters);
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }
    }
    public class RetrieveMany<T> : IService<T> where T : class, IDataObject
    {
        public string Error { get; set; }
        IUnityContainer Container;
        
        public DataImplementationType DataImplementationType { get; set; }
        public string Query { get; set; }
        public object[] args { get; set; }
        public int ItemsCount = 0;
        public object ExecuteAction(object obj, out string errMsg)
        {
            IList<T> result = default(IList<T>);
            errMsg = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    result = BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(Query, args).Result;
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    result = BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(args.ToKeyValuePairArray()).Result;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }
        public async Task<object> ExecuteActionAsync(object obj)
        {
            IList<T> result = default(IList<T>);
            //errMsg = string.Empty;
            try
            {
                //var kernel = new StandardKernel();
                //kernel.Load(Assembly.GetExecutingAssembly());
                if (DataImplementationType == DataImplementationType.NP)
                {
                    DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                    result = await BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(Query, args);
                }
                else
                {
                    DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                    var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                    //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                    result = await BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(args.ToKeyValuePairArray());
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }
        bool IsSearchParameters(KeyValuePair<string, object> pair)
        {
            string[] parameterKeys = new string[] { "sort", "direction", "page", "pageSize", "ItemsCount" };
            bool result = parameterKeys.Contains(pair.Key);
            return result;
        }
        public object ExecuteAction(out string errMsg, KeyValuePair<string, object>[] parameters, bool withPaging = false)
        {

            IList<T> result = default(IList<T>);
            errMsg = string.Empty;
            try
            {
                if (withPaging == false)
                {
                    //var kernel = new StandardKernel();
                    //kernel.Load(Assembly.GetExecutingAssembly());
                    if (DataImplementationType == DataImplementationType.NP)
                    {
                        DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                        var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                        //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                        result = BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(Query, args).Result;
                    }
                    else
                    {
                        DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                        var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                        //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                        result = BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(args.ToKeyValuePairArray()).Result;
                    }
                }
                else
                {
                    //string sort, string direction, int page, int pageSize, out int totalItemsCount
                    string sort = parameters.Where(x => x.Key == "sort").FirstOrDefault().Value.ToString();

                    string direction = parameters.Where(x => x.Key == "direction").FirstOrDefault().Value.ToString();
                    int page = int.Parse(parameters.Where(x => x.Key == "page").FirstOrDefault().Value.ToString());
                    int pageSize = int.Parse(parameters.Where(x => x.Key == "pageSize").FirstOrDefault().Value.ToString());
                    int ItemsCount = int.Parse(parameters.Where(x => x.Key == "ItemsCount").FirstOrDefault().Value.ToString());
                    //parameters.ToList().Remove(parameters.Where(x=> { return IsSearchParameters(x); }).FirstOrDefault());
                    var _real_parameters = parameters.Where(x => { return !IsSearchParameters(x); });
                    //_real_parameters.Remove(parameters.Where(x => { return IsSearchParameters(x); }).FirstOrDefault());
                    //var kernel = new StandardKernel();
                    //kernel.Load(Assembly.GetExecutingAssembly());
                    if (DataImplementationType == DataImplementationType.NP)
                    {
                        DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                        var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();

                        result = BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(Query, sort, direction, page, pageSize, _real_parameters.ToArray()).Result;
                    }
                    else
                    {
                        DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                        var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();

                        result = BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(_real_parameters.ToArray(), sort, direction, page, pageSize).Result;
                    }
                    parameters[parameters.Length - 1] = new KeyValuePair<string, object>("ItemsCount", ItemsCount);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }


        public async Task<object> ExecuteActionAsync(KeyValuePair<string, object>[] parameters, bool withPaging = true)
        {
            IList<T> result = default(IList<T>);
            try
            {
                if (withPaging == false)
                {
                    //var kernel = new StandardKernel();
                    //kernel.Load(Assembly.GetExecutingAssembly());
                    if (DataImplementationType == DataImplementationType.NP)
                    {
                        DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                        var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                        //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                        result = await BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(Query, args);
                    }
                    else
                    {
                        DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                        var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                        //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                        result = await BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(args.ToKeyValuePairArray());
                    }
                }
                else
                {
                    //string sort, string direction, int page, int pageSize, out int totalItemsCount
                    string sort = parameters.Where(x => x.Key == "sort").FirstOrDefault().Value.ToString();

                    string direction = parameters.Where(x => x.Key == "direction").FirstOrDefault().Value.ToString();
                    int page = int.Parse(parameters.Where(x => x.Key == "page").FirstOrDefault().Value.ToString());
                    int pageSize = int.Parse(parameters.Where(x => x.Key == "pageSize").FirstOrDefault().Value.ToString());
                    int ItemsCount = int.Parse(parameters.Where(x => x.Key == "ItemsCount").FirstOrDefault().Value.ToString());
                    //parameters.ToList().Remove(parameters.Where(x=> { return IsSearchParameters(x); }).FirstOrDefault());
                    var _real_parameters = parameters.Where(x => { return !IsSearchParameters(x); });
                    //_real_parameters.Remove(parameters.Where(x => { return IsSearchParameters(x); }).FirstOrDefault());
                    //var kernel = new StandardKernel();
                    //kernel.Load(Assembly.GetExecutingAssembly());
                    if (DataImplementationType == DataImplementationType.NP)
                    {
                        DataAccessDIConfiguration<INPBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INPBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                        var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INPBusinessObjectDAO<T>>();
                        //var BusinessObjectDAO = kernel.Get<INPBusinessObjectDAO<T>>();
                        result = await BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(Query, sort, direction, page, pageSize, _real_parameters.ToArray());
                    }
                    else
                    {
                        DataAccessDIConfiguration<INHBusinessObjectDAO<T>> DataAccessDIConfiguration = new DataAccessDIConfiguration<INHBusinessObjectDAO<T>>();DataAccessDIConfiguration.InstantiateService(Container);
                        var BusinessObjectDAO = DataAccessDIConfiguration.Container.Resolve<INHBusinessObjectDAO<T>>();
                        //var BusinessObjectDAO = kernel.Get<INHBusinessObjectDAO<T>>();
                        result = await BusinessObjectDAO.RetrieveDataObjectsByParametersAsync(_real_parameters.ToArray(), sort, direction, page, pageSize);
                    }
                    parameters[parameters.Length - 1] = new KeyValuePair<string, object>("ItemsCount", ItemsCount);
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return result;
        }
    }
    public class DIDAOImplementation<T> where T : class, IDataObject
    {
        public T _obj { get; set; }
        public KeyValuePair<string, object>[] _parameters { get; set; }
        public bool _hasParameters;
        public bool _isPaged = false;
        public string _errMsg { get; set; }
        IServiceProvider<T> _ServiceProvider { get; set; }
        public DIDAOImplementation()
        {
            Dictionary<string, IService<T>> services = new Dictionary<string, IService<T>>();
            IServiceCollection<T> serviceCollection = new ServiceCollection<T>(services);
            RegisterServices(serviceCollection);
            _ServiceProvider = new ServiceProvider<T>(services);
        }
        public DIDAOImplementation(T obj)
        {
            _obj = obj;
            Dictionary<string, IService<T>> services = new Dictionary<string, IService<T>>();
            IServiceCollection<T> serviceCollection = new ServiceCollection<T>(services);
            RegisterServices(serviceCollection);
            _ServiceProvider = new ServiceProvider<T>(services);
        }
        public DIDAOImplementation(KeyValuePair<string, object>[] parameters)
        {
            _parameters = parameters;
            Dictionary<string, IService<T>> services = new Dictionary<string, IService<T>>();
            IServiceCollection<T> serviceCollection = new ServiceCollection<T>(services);
            RegisterServices(serviceCollection);
            _ServiceProvider = new ServiceProvider<T>(services);
        }
        public object Execute(string operation)
        {
            _errMsg = string.Empty;
            string errMsg = string.Empty;
            object response = new object();
            var provider = _ServiceProvider.GetByName(operation);
            if (_hasParameters == false)
            {
                response = provider.ExecuteAction(_obj, out errMsg);
            }
            else
            {
                response = provider.ExecuteAction(out errMsg, _parameters, _isPaged);
            }
            _errMsg = errMsg;
            return response;
        }

        public async Task<object> ExecuteAsync(string operation)
        {
            _errMsg = string.Empty;
            object response = new object();
            var provider = _ServiceProvider.GetByName(operation);
            if (_hasParameters == false)
            {
                response = await provider.ExecuteActionAsync(_obj);
            }
            else
            {
                response = await provider.ExecuteActionAsync(_parameters, _isPaged);
            }
            _errMsg = provider.Error;// errMsg;
            return response;
        }



        void RegisterServices(IServiceCollection<T> serviceCollection)
        {
            serviceCollection.AddNamedService("Save", new Save<T>());
            serviceCollection.AddNamedService("Update", new Update<T>());
            serviceCollection.AddNamedService("Retrieve", new Retrieve<T>());
            serviceCollection.AddNamedService("RetrieveMany", new RetrieveMany<T>());
            serviceCollection.AddNamedService("Commit", new Commit<T>());
        }
    }
    public interface IService<T> where T : IDataObject
    {
        string Error { get; set; }
        DataImplementationType DataImplementationType { get; set; }
        object ExecuteAction(object obj, out string errMsg);
        Task<object> ExecuteActionAsync(object obj);
        object ExecuteAction(out string errMsg, KeyValuePair<string, object>[] parameters, bool withPaging = true);
        Task<object> ExecuteActionAsync(KeyValuePair<string, object>[] parameters, bool withPaging = true);
    }
    public interface IServiceCollection<T> where T : IDataObject
    {
        void AddNamedService(string name, IService<T> service);
    }
    public class ServiceCollection<T> : IServiceCollection<T> where T : IDataObject
    {
        private readonly IDictionary<string, IService<T>> services;

        public ServiceCollection(IDictionary<string, IService<T>> services)
        {
            this.services = services;
        }

        public void AddNamedService(string name, IService<T> service)
        {
            if (services.ContainsKey(name))
            {
                this.services[name] = service;
            }
            else
            {
                this.services.Add(name, service);
            }
        }
    }
    public interface IServiceProvider<T> where T : IDataObject
    {
        IService<T> GetByName(string name);
    }
    public class ServiceProvider<T> : IServiceProvider<T> where T : IDataObject
    {
        private readonly IDictionary<string, IService<T>> services;

        public ServiceProvider(IDictionary<string, IService<T>> services)
        {
            this.services = services;
        }

        public IService<T> GetByName(string name)
        {
            if (this.services.ContainsKey(name))
            {
                return this.services[name];
            }
            return null;
        }
    }

    public static class KeyValuePairExtension
    {
        public static object[] ToDynamicObject(this KeyValuePair<string, object>[] args)
        {
            dynamic item = new ExpandoObject();
            var dItem = item as IDictionary<String, object>;

            for (int buc = 0; buc < args.Length; buc++)
                dItem.Add(args[buc].Key, args[buc].Value);
            return item;
        }
    }
    public static class ObjectArrayExtension
    {
        public static KeyValuePair<string, object>[] ToKeyValuePairArray(this object[] args)
        {
            KeyValuePair<string, object>[] result = null;// new KeyValuePair<string, object>[]();
            Dictionary<string, object> dict =
                    args.GetType()
                      .GetProperties()
                      .ToDictionary(p => p.Name, p => p.GetValue(args, null));
            result = dict.ToArray();
            return result;
        }
    }
}
