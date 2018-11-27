using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Models
{
    public class LoginModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
