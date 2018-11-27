using CoreBusiness.Common;
using CoreBusiness.Data;
using CoreBusiness.Models;
using CoreBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.App
{
    class Program
    {
        static void Main3(string[] args)
        {
            string cipherNotation = DateTime.Now.ToString("ddMMyyyyhhmmssss");
            User usr = new User();
            usr.Username = "oigbinosun";
            var grid = PrintGrid(cipherNotation);
            Console.WriteLine(Environment.NewLine + Environment.NewLine);
            var challenge = PrintGridChallenge();
            Console.WriteLine($"{Environment.NewLine}Enter Values Seperated by Commas{Environment.NewLine}");
            var response = Console.ReadLine();
            TestChallenge(grid, challenge, response);
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            TestCRUD();
        }
        static string[,] PrintGrid(string cipherNotation)
        {

            string[,] arr = SecuritySystem.GenerateGridByUserName("oigbinosun", cipherNotation);

            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", arr[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
            return arr;
        }

        static string[] PrintGridChallenge()
        {
            string[] arr = SecuritySystem.GenerateGridChallenge(true);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"[{arr[i]}]");
            }
            return arr;
        }
        static void TestChallenge(string[,] grid, string[] challenge, string response)
        {
            var responseArray = response.Split(',');
            if (SecuritySystem.AuthenticateGridChallenge(grid, challenge, responseArray))
            {
                Console.WriteLine("Challenge Accepted");
            }
            else
            {
                Console.WriteLine("Challenge Rejected");
            }
        }
        static void Main2(string[] args)
        {
            //TestCRUD();
            TestSecurity();
            Console.ReadLine();
        }
        static void TestSecurity()
        {
            var grid = SecuritySystem.GenerateGridByUserName("John Ubah");
        }
        static void TestCRUD()
        {
            UserModel usr = new UserModel();
            usr.name = "John Ubah";
            usr.FirstName = "John";
            usr.LastName = "Ubah";
            usr.password = "password";
            usr.email = "jb@c.com";
            UserSystem u_system = new UserSystem();
            var result = u_system.RegisterUser(usr).Result;
            User n_user = u_system.RetrieveByEmailAsync("jb@c.com").Result;
            n_user.FirstName = "Jonathan";
            //n_user.shoko = "My  foot";
            result = u_system.UpdateUserAsync(n_user).Result;
            var new_user = n_user.Clone(n_user, x => x.SecurityGrid);
            Console.ReadLine();
        }
    }
}
