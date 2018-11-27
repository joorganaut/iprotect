using CoreBusiness;
using CoreBusiness.Models;
using CoreBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAdmin.Controllers
{
    public class UserController : ApiController
    {
        private UserSystem System = new UserSystem();
        [HttpPost]
        [Route("api/User/RegisterNewUser")]
        public async Task<bool> RegisterUser(IViewModel viewModel)
        {
            var result = await System.RegisterUser(viewModel as UserModel);
            return result;
        }
    }
}
