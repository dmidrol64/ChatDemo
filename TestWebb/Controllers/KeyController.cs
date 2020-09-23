using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebb.Models;

namespace TestWebb.Controllers
{
    public class KeyController : Controller
    {
        public object Getkeys(int userId)
        {
            var context = ConnectionService.GetContext();

            var getUserId = context.Users.Where(gui => gui.Id == userId).FirstOrDefault();
            if(getUserId != null)
            {
                var allKeys = (from setKeys in context.Keys where setKeys.UserId == userId select setKeys);
                return new { error = false, message = allKeys.ToString() };
            }
            return new { error = true, message = " Ошибка поиска ключей" };
        }
            public object AddKeys(int userId, string key, string token, int tokenId)
        {
            var context = ConnectionService.GetContext();
            var check = CheckToken.Check(tokenId, userId,token);
            if(check == false)
            {
                return new { error = true, messageError = "Неправильный токен" };
            }
            var fKey = context.Keys.Where(fk => fk.UserId == userId && fk.Key == key).FirstOrDefault();
            if (fKey == null)
            {
                KeyStorage keyStorage = new KeyStorage()
                {
                    UserId = userId,
                    Key = key   
                };
                context.Keys.Add(keyStorage);
                context.SaveChanges();
                return new { error = false, userId = userId, key = key};
            
            }
            return new  { error = true, messageError = "Ошибка" };
        

        }
        public object RemoveKeys(int id, int userId, int tokenId,string token)
        {
            var context = ConnectionService.GetContext();
            var check = CheckToken.Check(tokenId, userId, token);
            if (check == false)
            {
                return new { error = true, messageError = "Неправильный токен" };
            }
            var rem = context.Keys.Where(r => r.Id == id && r.UserId == userId).FirstOrDefault();

            if(rem != null)
            {
                context.Keys.Remove(rem);
                context.SaveChanges();
                return new { error = false, userId = userId };
                
            }
            return new { error = true, messageError = "Ошибка" };

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
