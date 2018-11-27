using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBusiness.Models
{
    public class Challenge
    {
        public string[] Request { get; set; }
        public string[] Response { get; set; }
        public int Size { get; set; }
        public Challenge(int size)
        {
            Size = size;
            Request = new string[size];
            Response = new string[size];
        }
    }
}
