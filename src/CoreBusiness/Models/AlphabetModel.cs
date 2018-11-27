using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Models
{
    [Serializable]
    public class AlphabetModel : IExternalObjectModel
    {
        public string Alphabet { get; set; }
        public string Medication { get; set; }
        public string Description { get; set; }
        public string error { get; set; }
    }
}
