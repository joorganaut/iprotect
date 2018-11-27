using AXAMansard.Framework.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness
{
    public interface IExternalObject : IDataObject
    {
    }
    public interface IExternalObjectModel
    {
        string error { get; set; }
    }
}
