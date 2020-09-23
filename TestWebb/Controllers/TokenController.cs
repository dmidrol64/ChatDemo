using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebb.Models;
using System.Text;
using System.Security.Cryptography;

namespace TestWebb.Controllers
{
    public class TokenController : Controller
    {
        public Token getToken(string login, string password)
        {

            var context = ConnectionService.GetContext();
            //UserData user = new UserData();
            Token token = new Token();
            SHA512 shaM = new SHA512Managed();
            byte[] pass = Encoding.UTF8.GetBytes(password);
            pass = shaM.ComputeHash(pass);
            //string hashString = Encoding.UTF8.GetString(pass);
            string hashString = Convert.ToBase64String(pass);
            var TokenForUser = context.Users.Where(u => u.Login == login && u.Password == hashString).FirstOrDefault();
           
            if (TokenForUser != null)
            {
                token.UserId = TokenForUser.Id;
                Random rnd = new Random();
                byte[] mass = new byte[256];
                rnd.NextBytes(mass);

                var s = Convert.ToBase64String(mass);
                s = s.Replace('/', '0');
                s = s.Replace('+', '1');
                s = s.Replace('=', '2');
                token.TokenId = s;
                context.Tokens.Add(token);
                context.SaveChanges();

                
            }
            return token;
            //context.Tokens.Add();
        }
        public object removeToken(int id,int userId, string tokenId)
        {
            var context = ConnectionService.GetContext();
            var token = context.Tokens.Where(t=>t.Id == id && t.TokenId == tokenId && t.UserId == userId).FirstOrDefault();
            if (token != null)
            {
                context.Tokens.Remove(token);
                context.SaveChanges();
                return new { error = false, token=token.Id };
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}