using CoreBusiness.Data;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusinessLogic
{
    public class UFunctionSystem
    {
        public string Error;
        StandardKernel kernel = new StandardKernel();

        public UFunctionSystem()
        {
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        public async Task<bool> SaveFunctionAsync(UFunction function)
        {
            bool result = false;
            Error = string.Empty;
            function.DateCreated = DateTime.Now;
            function.DateLastModified = DateTime.Now;
            var Implementation = kernel.Get<IDIDAOImplementation<UFunction>>();
            Implementation._obj = function;
            var response = await Implementation.ExecuteAsync("Save");
            if (bool.TryParse(response.ToString(), out result))
            {
                Error = Implementation._errMsg;
            }
            return result;
        }
        public async Task<bool> UpdateFunctionAsync(UFunction function)
        {
            bool result = false;
            Error = string.Empty;
            function.DateLastModified = DateTime.Now;
            var Implementation = kernel.Get<IDIDAOImplementation<UFunction>>();
            Implementation._obj = function;
            var response = await Implementation.ExecuteAsync("Update");
            if (bool.TryParse(response.ToString(), out result))
            {
                Error = Implementation._errMsg;
            }
            return result;
        }
        public async Task<bool> SaveRoleFunctionAsync(RFunction rFunction)
        {
            bool result = false;
            Error = string.Empty;
            rFunction.DateCreated = DateTime.Now;
            rFunction.DateLastModified = DateTime.Now;
            var Implementation = kernel.Get<IDIDAOImplementation<RFunction>>();
            Implementation._obj = rFunction;
            var response = await Implementation.ExecuteAsync("Save");
            if (bool.TryParse(response.ToString(), out result))
            {
                Error = Implementation._errMsg;
            }
            return result;
        }
        public async Task<bool> UpdateRoleFunctionAsync(UserRole Role)
        {
            bool result = false;
            Error = string.Empty;
            Role.DateLastModified = DateTime.Now;
            var Implementation = kernel.Get<IDIDAOImplementation<UserRole>>();
            Implementation._obj = Role;
            var response = await Implementation.ExecuteAsync("Update");
            if (bool.TryParse(response.ToString(), out result))
            {
                Error = Implementation._errMsg;
            }
            return result;
        }
        public async Task<List<RFunction>> RetrieveByRole(long role)
        {
            KeyValuePair<string, object>[] kvp = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("RoleID", role) };
            var Implementation = kernel.Get<IDIDAOImplementation<RFunction>>();
            Implementation._hasParameters = true;
            Implementation._parameters = kvp;
            var response = await Implementation.ExecuteAsync("RetrieveAll");
            Error = Implementation._errMsg;
            return response as List<RFunction>;
        }
    }
}
