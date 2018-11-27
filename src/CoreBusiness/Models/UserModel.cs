using CoreBusiness.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Models
{
    [Serializable]
    public class UserModel : BaseViewModel
    {
        public bool isauthenticated { get; set; }
        //id, name, type, fontName, fontSize, fontColor, sex, age, friendsList, status, memberType
        public string username { get; set; }
        public string password { get; set; }
        public string pin { get; set; }
        public string ConnectionId { get; set; }
        public string LoginTime { get; set; }
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string name { get; set; }
        public List<UserModel> friendsList { get; set; }
        public List<Account> accountList { get; set; }
        public List<Subscription> subscriptions { get; set; }
        public string fontName { get; set; }
        public string fontSize { get; set; }
        public string fontColor { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public string status { get; set; }
        public string memberType { get; set; }
        public string avator { get; set; }
        public string error { get; set; }
        public string email { get; set; }
        //public user Login(string UserName)
        //{ 


        //}

    }
    public class UsersRestricted
    {

        public int id { get; set; }
        public string name { get; set; }
        public string roomName { get; set; }
        public Restriction restriction { get; set; }

        public DateTime time { get; set; }

        public string restrictekBy { get; set; }
    }
    public enum Restriction
    {
        BAN, MUTE, KICK
    }
}
