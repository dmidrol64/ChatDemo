using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestWebb.Models;

namespace TestWebb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
       
        public object CreateNewUser(string login, string password)
        {
            SHA512 shaM = new SHA512Managed();
            var context = ConnectionService.GetContext();
            byte[] pass = Encoding.UTF8.GetBytes(password);
            pass = shaM.ComputeHash(pass);
            //string hashString = Encoding.UTF8.GetString(pass);
           string hashString = Convert.ToBase64String(pass);


            UserData user = new UserData() { Login = login.ToLower(), Password = hashString };
            context.Users.Add(user);
            context.SaveChanges();
            return user.Id;
        }
         
      
        public object CreateProfile(int id, string mail, string name, string surname,string token)
        {
            var context = ConnectionService.GetContext();
            var uti = context.Tokens.Where(u => u.UserId==id).FirstOrDefault();
            if (uti != null && uti.TokenId == token)
            {

                Profile profile = new Profile() { Mail = mail, Name = name, Surname = surname, UserId = id };
      
                    context.Profiles.Add(profile);
                    context.SaveChanges();
                
                return profile.Id;
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
    }
}
