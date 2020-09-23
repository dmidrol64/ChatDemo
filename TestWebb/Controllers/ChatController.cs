using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebb.Models;

namespace TestWebb.Controllers
{
    public class ChatController : Controller
    {
        public object CreateChat(int userid,string name, string token)
        {
            var context = ConnectionService.GetContext();

            var tokenUser = context.Tokens.Where(tc => tc.UserId==userid).FirstOrDefault();
            if (tokenUser != null && tokenUser.TokenId == token)
            {
                Chat chat = new Chat() { UserId = userid, Name = name };
                context.Chats.Add(chat);
                context.SaveChanges();
                return new{chatName = name,chatId=chat.Id,owener=userid,error=false};
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
        public object DeleteChat(int userId, int chatid, string token)
        {
            var context = ConnectionService.GetContext();
            var delchat = context.Tokens.Where(dc => dc.UserId == userId).FirstOrDefault();
            var chat = context.Chats.Where(c => c.Id == chatid).FirstOrDefault();
           
                if (delchat!=null&&chat.UserId == delchat.UserId && delchat.TokenId == token)
                {
                    context.Chats.Remove(chat);
                    context.SaveChanges();
                return new {error=false,chatId=chat.Id };
                }
            return new {error=true, messageError = "Указанный Вами токен не подходит" }; 

        }
      

        public object AddUserToChat(int userId,int chatId, int owenerId,string token)
        {
            var context = ConnectionService.GetContext();
            
            var userChat = context.UserChats.Where(uc => uc.ChatId == chatId && uc.UserId == userId).FirstOrDefault();
            var addUserChat = context.Tokens.Where(auc => auc.UserId == userId).FirstOrDefault();
            if (userChat == null&&addUserChat!=null)
            {
                var chat = context.UserChats.Where(c => c.ChatId == chatId).FirstOrDefault();
               
                if (chat?.UserId == owenerId&&addUserChat.UserId==chat.UserId&&addUserChat.TokenId==token)
                {
                    userChat = new UserChat();
                    userChat.UserId = userId;
                    userChat.ChatId = chatId;
                    context.UserChats.Add(userChat);
                    context.SaveChanges();
                    return new { error = false, userId=userId,user=owenerId };
                }
               
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
        public object DeleteUserFromChat(int chatId,int userId,int owenerId,string token)
        {
            var context = ConnectionService.GetContext();
            var delUserChat = context.Tokens.Where(duc => duc.UserId == userId).FirstOrDefault();
            UserChat userChat = context.UserChats.Where(du => du.ChatId == chatId && du.UserId == userId).FirstOrDefault();
            if (userChat != null&&delUserChat!=null)
            {
                if(userId == owenerId&&delUserChat.UserId==userId&&delUserChat.TokenId==token)
                {

                    context.UserChats.Remove(userChat);
                    context.SaveChanges();
                    return true;
                }
                else { 
               var chat = context.Chats.Where(o => o.Id == chatId).FirstOrDefault();
                    if (chat.UserId == owenerId)
                    {
                        context.UserChats.Remove(userChat);
                        context.SaveChanges();
                        return new { error = false, chatId = chat.Id,userId=userId,user=owenerId };
                    }
                }
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
        public object AddToExistChat(int chatId,int userId,string token)
        {
            
            var context = ConnectionService.GetContext();
            var findUserInTokens = context.Tokens.Where(t => t.UserId == userId).FirstOrDefault();
            if (findUserInTokens == null || findUserInTokens.TokenId != token)
            {
                return new { error = true, message = "неверный токен" };
            }
            var findChat = context.Chats.Where(fch => fch.Id == chatId).FirstOrDefault();

            if (findChat != null)
            {
                var findUser = context.UserChats.Where(fu => fu.UserId == userId && fu.ChatId==chatId).FirstOrDefault();
                var banList = context.Bans.Where(ban => ban.UserId == userId && ban.ChatId == chatId).FirstOrDefault();
                if (findUser == null&&findUser.ChatId!=banList.ChatId&&findUser.UserId!=banList.UserId)
                {
                    UserChat userChat = new UserChat();
                    userChat.UserId = userId;
                    userChat.ChatId = chatId;
                    context.UserChats.Add(userChat);
                    context.SaveChanges();

                }
                else
                {
                    return new { error = true, message = "Доступ закрыт" };
                }
                return new { error = false, chat = chatId, user = userId };
            }
            return new { error = true, message = "ошибка добавления" };
        }

        public object DelitingFromChat(int chatId, int userId, string token)
        {
            var context = ConnectionService.GetContext();
            var Tkn = context.Tokens.Where(t => t.UserId == userId).FirstOrDefault();
            if (Tkn == null || Tkn.TokenId == token)
            {
                return new { error = true, message = "неверный токен" };
            }
            var findChat = context.Chats.Where(fch => fch.Id == chatId).FirstOrDefault();
            if (findChat != null)
            {
                var findUser = context.UserChats.Where(fu => fu.UserId == userId && fu.ChatId == chatId).FirstOrDefault();
                if (findUser != null)
                {
                    UserChat userChat = new UserChat();
                    userChat.ChatId = chatId;
                    userChat.UserId = userId;
                    context.UserChats.Remove(userChat);
                    context.SaveChanges();
                }
                return new { error = false, chat = chatId, user = userId };
            }
            return new { error = true, message = "ошибка удаления" };
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}