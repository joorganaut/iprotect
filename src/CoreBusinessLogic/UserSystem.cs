using AXAMansard.Framework.Utility;
using CoreBusiness.Data;
using CoreBusiness.Models;
using CoreBusinessLogic.Resources;
using Ninject;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CoreBusinessLogic
{
    public class UserSystem
    {
        public string Error;
        StandardKernel kernel = new StandardKernel();
        
        public UserSystem()
        {
            kernel.Load(Assembly.GetExecutingAssembly());
        }
        //static List<Settings> Settings = HttpContext.Current.Application["Settings"] as List<Settings>;
        


        public async Task<bool> SaveUserAsync(User user)
        {
            bool result = false;
            Error = string.Empty;
            user.DateCreated = DateTime.Now;
            user.DateLastModified = DateTime.Now;
            var Implementation = kernel.Get <IDIDAOImplementation<User>>();
            Implementation._obj = user;
            var response = await Implementation.ExecuteAsync("Save");
            if (bool.TryParse(response.ToString(), out result))
            {
                Error = Implementation._errMsg;
            }
            return result;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            bool result = false;
            Error = string.Empty;
            user.DateLastModified = DateTime.Now;
            var Implementation = kernel.Get<IDIDAOImplementation<User>>();
            Implementation._obj = user;
            var response = await Implementation.ExecuteAsync("Update");
            if (bool.TryParse(response.ToString(), out result))
            {
                Error = Implementation._errMsg;
            }
            return result;
        }
        public User RetrieveByUsername(string username, out string errMsg)
        {
            errMsg = string.Empty;
            KeyValuePair<string, object>[] kvp = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("Username", username) };
            var Implementation = kernel.Get<IDIDAOImplementation<User>>();
            Implementation._hasParameters = true;
            Implementation._parameters = kvp;
            var response = Implementation.Execute("Retrieve");
            return response as User;
        }
        public User RetrieveByEmail(string email, out string errMsg)
        {
            errMsg = string.Empty;
            KeyValuePair<string, object>[] kvp = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("Email", email) };
            var Implementation = kernel.Get<IDIDAOImplementation<User>>();
            Implementation._hasParameters = true;
            Implementation._parameters = kvp;
            var response = Implementation.Execute("Retrieve");
            return response as User;
        }


        public async Task<User> RetrieveByEmailAsync(string email)
        {
            KeyValuePair<string, object>[] kvp = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("Email", email) };
            var Implementation = kernel.Get<IDIDAOImplementation<User>>();
            Implementation._hasParameters = true;
            Implementation._parameters = kvp;
            var response = await Implementation.ExecuteAsync("Retrieve");
            Error = Implementation._errMsg;
            return response as User;
        }
        public User RetrieveByUsernameAndEmail(string username, string email, out string errMsg)
        {
            errMsg = string.Empty;
            KeyValuePair<string, object>[] kvp = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("Username", username), new KeyValuePair<string, object>("Email", email) };
            var Implementation = kernel.Get<IDIDAOImplementation<User>>();
            Implementation._hasParameters = true;
            Implementation._parameters = kvp;
            var response = Implementation.Execute("Retrieve");
            return response as User;
        }
        public async Task<User> RetrieveByUsernameAndEmailAsync(string username, string email)
        {
            KeyValuePair<string, object>[] kvp = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("Username", username), new KeyValuePair<string, object>("Email", email) };
            var Implementation = kernel.Get<IDIDAOImplementation<User>>();
            Implementation._hasParameters = true;
            Implementation._parameters = kvp;
            var response = await Implementation.ExecuteAsync("Retrieve");
            Error = Implementation._errMsg;
            return response as User;
        }

        public void CommitChanges()
        {
            var Implementation = kernel.Get<IDIDAOImplementation<User>>();
            //Implementation.Execute("Commit");
        }

        public async Task CommitChangesAsync()
        {
            var Implementation = kernel.Get<IDIDAOImplementation<User>>();
            //await Implementation.ExecuteAsync("Commit");
        }


        public async Task<bool> AuthenticateUser(UserModel model)
        {
            bool result = false;
            var usr = await RetrieveByUsernameAndEmailAsync(model.username, model.email);
            if (usr != null)
            {
                if (usr.IsEnabled == false)
                {
                    model.error = $"User with Username {model.username} is Locked out";
                    //return false;
                }
                if (usr.Password == new MD5Password().CreateSecurePassword(model.password))
                {
                    model.isauthenticated = true;
                    model.name = $"{usr.FirstName} {usr.LastName}";

                    usr.LastLoginDate = DateTime.Now;
                    await UpdateUserAsync(usr);

                    model.error = Error;
                    await CommitChangesAsync();
                    result = true;
                }
                else
                {
                    usr.NumberOfFailedAttempts = usr.NumberOfFailedAttempts + 1;
                    //if (Settings != null && usr.NumberOfFailedAttempts >= int.Parse(Settings.Where(x => x.Name == Constants.FailedLoginAttempts).FirstOrDefault().Value))
                    //{
                    //    usr.IsEnabled = false;
                    //}
                    await UpdateUserAsync(usr);
                    model.error = Error;
                    await CommitChangesAsync();
                    model.error = $"Unable to Login with Username: {model.username}";
                }
            }
            else
            {
                model.error = $"Unable to Login with Username: {model.username}";
            }
            return result;
        }

        public async Task<bool> RegisterUser(UserModel model)
        {
            bool result = false;
            var usr = await RetrieveByEmailAsync(model.email);
            if (usr != null)
            {
                model.error = $"User with Email: {usr.Email} already Exists";
                return result;
            }
            usr = new User();
            usr.Username = model.username;
            usr.Email = model.email;
            usr.FirstName = model.FirstName;
            usr.LastName = model.LastName;
            usr.DOB = model.DOB;
            usr.Name = model.name;
            usr.Password = new MD5Password().CreateSecurePassword(model.password);
            if (await SaveUserAsync(usr))
            {
                model.error = Error;
                CommitChanges();
                result = true;
            }
            return result;
        }
        public async Task<bool> UnlockUser(UserModel model)
        {
            bool result = false;
            string errMsg = string.Empty;

            var usr = RetrieveByEmail(model.email, out errMsg);
            if (usr != null)
            {
                usr.IsEnabled = true;
                await UpdateUserAsync(usr);
                model.error = errMsg;
                await CommitChangesAsync();
                model.isauthenticated = true;
                result = true;
            }
            return result;
        }
        public async Task<bool> ChangePassword(UserModel model, string newPassword)
        {
            bool result = false;
            var usr = await RetrieveByEmailAsync(model.email);
            if (usr != null)
            {
                if (model.password == new MD5Password().GetPasswordInClear(usr.Password))
                {
                    usr.Password = new MD5Password().CreateSecurePassword(newPassword);
                    await UpdateUserAsync(usr);
                    model.error = Error;
                    await CommitChangesAsync();
                    model.isauthenticated = true;
                    result = true;
                }
                else
                {
                    model.error = $"Old Password is Wrong";
                }
            }
            return result;
        }


        public async Task<bool> ChangePin(UserModel model, string pin)
        {
            bool result = false;
            var usr = await RetrieveByEmailAsync(model.email);
            MD5Password passEnc = new MD5Password();
            if (usr != null)
            {
                if (passEnc.CreateSecurePassword(model.pin) == new MD5Password().CreateSecurePassword(usr.Pin))
                {
                    usr.Password = new MD5Password().CreateSecurePassword(pin);
                    await UpdateUserAsync(usr);
                    model.error = Error;
                    await CommitChangesAsync();
                    model.isauthenticated = true;
                    result = true;
                }
                else
                {
                    model.error = $"Old PIN is Wrong";
                }
            }
            return result;
        }
        public async Task SendUserGrid(User usr)
        {
            var gridTemplate = ScriptLoader.GetString("GridTemplate.html");
            var grid = SecuritySystem.GenerateGridByUserName(usr.Username, usr.Pin);
            MailModel mModel = new MailModel();
            mModel.To = usr.Email;
            mModel.Body = gridTemplate.Replace("{grid}", GetGridTable(grid));
            MailSystem mSystem = new MailSystem();
            await mSystem.SendGridAsync(mModel);
        }
        public Challenge GenerateChallengeForUser(User usr)
        {
            return SecuritySystem.GenerateGridChallenge();
        }
        public bool AuthenticateGridChallengeForUser(User usr, Challenge challenge)
        {
            return SecuritySystem.AuthenticateGridChallengeUsingCipherNotation(usr, challenge);
        }
        private string GetGridTable(string[,] gridValue)
        {
            StringBuilder builder = new StringBuilder();
            int rowLength = gridValue.GetLength(0);
            int colLength = gridValue.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                builder.AppendLine($"<tr>");
                for (int j = 0; j < colLength; j++)
                {
                    var value = gridValue[i, j];
                    builder.AppendLine($"<td>{value}</td>");
                }
                builder.AppendLine($"</tr>");
                builder.AppendLine(Environment.NewLine);
            }
            return builder.ToString();
        }
    }
}
