using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebb.Models;

namespace TestWebb.Controllers
{
    public class BanListController : Controller
    {
        public object AddToBanList(int chatId, int idBanningUser, string token, int owner)
        {
            var context = ConnectionService.GetContext();
            var Tkn = context.Tokens.Where(t => t.UserId == owner).FirstOrDefault();
            if (Tkn == null || Tkn.TokenId != token)
            {
                return new { error = true, message = "неверный токен" };
            }
            
            var findChat = context.Chats.Where(fch => fch.Id == chatId).FirstOrDefault();
            if (findChat != null)
            {
                if (findChat.UserId == idBanningUser) {
                    return new { error = true, message = "Не возможно добавить владельца чата в банлист" };
                }

                if (findChat.UserId != owner)
                {
                    return new { error = true, message = "вы не являетесь владельцем чата, не достаточно прав" };
                }
                var findUser = context.UserChats.Where(fu => fu.UserId == idBanningUser && fu.ChatId == chatId).FirstOrDefault();
                if (findUser != null)
                {
                    BanList banList = new BanList();
                    banList.ChatId = chatId;
                    banList.UserId = idBanningUser;
                    context.Bans.Add(banList);
                    context.UserChats.Remove(findUser);
                    context.SaveChanges();
                }
                else
                {
                    return new { error = true, message = "пользователь отсутствует в чате" };
                }
                return new { error = false, chat = chatId, user = idBanningUser };
            }
            return new { error = true, message = "чат отсутствует" };
        }


        public object RecoveryFromBanlist(int chatId, int idBanningUser, string token, int owner)
        {

            var context = ConnectionService.GetContext();
            var Tkn = context.Tokens.Where(t => t.UserId == owner).FirstOrDefault();
            if (Tkn == null || Tkn.TokenId != token)
            {
                return new { error = true, message = "неверный токен" };
            }
            var search = context.Bans.Where(s => s.ChatId == chatId && s.UserId == idBanningUser).FirstOrDefault();
            var findChat = context.Chats.Where(fch => fch.Id == chatId).FirstOrDefault();
            if (findChat != null)
            {

                if (findChat.UserId != owner)
                {
                    return new { error = true, message = "вы не являетесь владельцем чата, не достаточно прав" };
                }
                var findUser = context.UserChats.Where(fu => fu.UserId == idBanningUser && fu.ChatId == chatId).FirstOrDefault();
                if (findUser == null && search != null)
                {
                    UserChat userChat = new UserChat();
                    userChat.ChatId = chatId;
                    userChat.UserId = idBanningUser;
                    //BanList banList = new BanList();
                    //banList.ChatId = chatId;
                    //banList.UserId = idBanningUser;
                    context.Bans.Remove(search);
                    context.UserChats.Add(userChat);
                    context.SaveChanges();

                    return new { error = false, chat = chatId, user = idBanningUser };
                }
                else
                {
                    return new { error = true, message = "пользователь присутствует в чате" };
                }

                
            }

            return new { error = true, message = "чат не найден" };
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
    
    



