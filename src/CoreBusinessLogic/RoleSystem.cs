using CoreBusiness.Data;
using CoreBusiness.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusinessLogic
{
    public class RoleSystem
    {
        public string Error;
        StandardKernel kernel = new StandardKernel();

        public RoleSystem()
        {
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        public async Task<bool> SaveRoleAsync(Role Role)
        {
            bool result = false;
            Error = string.Empty;
            Role.DateCreated = DateTime.Now;
            Role.DateLastModified = DateTime.Now;
            var Implementation = kernel.Get<IDIDAOImplementation<Role>>();
            Implementation._obj = Role;
            var response = await Implementation.ExecuteAsync("Save");
            if (bool.TryParse(response.ToString(), out result))
            {
                Error = Implementation._errMsg;
            }
            return result;
        }
        public async Task<bool> UpdateRoleAsync(Role Role)
        {
            bool result = false;
            Error = string.Empty;
            Role.DateLastModified = DateTime.Now;
            var Implementation = kernel.Get<IDIDAOImplementation<Role>>();
            Implementation._obj = Role;
            var response = await Implementation.ExecuteAsync("Update");
            if (bool.TryParse(response.ToString(), out result))
            {
                Error = Implementation._errMsg;
            }
            return result;
        }
        public async Task<bool> SaveUserRoleAsync(UserRole Role)
        {
            bool result = false;
            Error = string.Empty;
            Role.DateCreated = DateTime.Now;
            Role.DateLastModified = DateTime.Now;
            var Implementation = kernel.Get<IDIDAOImplementation<UserRole>>();
            Implementation._obj = Role;
            var response = await Implementation.ExecuteAsync("Save");
            if (bool.TryParse(response.ToString(), out result))
            {
                Error = Implementation._errMsg;
            }
            return result;
        }
        public async Task<bool> UpdateUserRoleAsync(UserRole Role)
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
        public async Task<List<UserRole>> RetrieveByUsername(string username)
        {
            KeyValuePair<string, object>[] kvp = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("Username", username)};
            var Implementation = kernel.Get<IDIDAOImplementation<Role>>();
            Implementation._hasParameters = true;
            Implementation._parameters = kvp;
            var response = await Implementation.ExecuteAsync("RetrieveAll");
            Error = Implementation._errMsg;
            return response as List<UserRole>;
        }
        public async Task<List<Role>> Retrieve(long id)
        {
            KeyValuePair<string, object>[] kvp = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("ID", id) };
            var Implementation = kernel.Get<IDIDAOImplementation<Role>>();
            Implementation._hasParameters = true;
            Implementation._parameters = kvp;
            var response = await Implementation.ExecuteAsync("Retrieve");
            Error = Implementation._errMsg;
            return response as List<Role>;
        }
        public async Task<List<UserRole>> RetrieveByUser(long user)
        {
            KeyValuePair<string, object>[] kvp = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("UserID", user) };
            var Implementation = kernel.Get<IDIDAOImplementation<Role>>();
            Implementation._hasParameters = true;
            Implementation._parameters = kvp;
            var response = await Implementation.ExecuteAsync("RetrieveAll");
            Error = Implementation._errMsg;
            return response as List<UserRole>;
        }
        public async Task<bool> AddRoleToUser(UserModel model, long roleID)
        {
            bool result = false;
            var usr = await RetrieveByUsername(model.username);
            if (usr != null && usr.Count > 0)
            {
                var oldRole = usr.Where(x => x.RoleID == roleID).FirstOrDefault();
                if (oldRole != null)
                {
                    model.error = $"Role already exists for this user: {model.username} ";
                    return result;
                }
            }
            var role = await Retrieve(roleID);
            if (role == null || role.Count <= 0)
            {
                model.error = $"Role does not exists";
                return result;
            }
            UserRole newRole = new UserRole() {
                DateCreated = DateTime.Now,
                RoleID = role[0].ID,
                Username = model.username,
                UserID = model.id,
                RoleName = role[0].Name,
                IsEnabled = true,
            };
            if (await SaveUserRoleAsync(newRole))
            {
                model.error = Error;
                result = true;
            }
            return result;
        }
        public async Task<bool> RemoveRoleFromUser(UserModel model, long roleID)
        {
            bool result = false;
            var usr = await RetrieveByUsername(model.username);
            if (usr != null && usr.Count > 0)
            {
                var oldRole = usr.Where(x => x.RoleID == roleID).FirstOrDefault();
                if (oldRole == null)
                {
                    model.error = $"Role does not exist for this user: {model.username} ";
                    return result;
                }
                oldRole.IsEnabled = false;
                if (await UpdateUserRoleAsync(oldRole))
                {
                    model.error = Error;
                    result = true;
                }
            }            
            return result;
        }
    }
}
