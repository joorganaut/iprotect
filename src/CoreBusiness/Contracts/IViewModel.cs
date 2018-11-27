using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness
{
    public interface IViewModel
    {
        string ErrorMessage { get; set; }
    }
    public class BaseViewModel : IViewModel
    {
        public string ErrorMessage { get; set; }
    }
}
