using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebb.Models;

namespace TestWebb.Controllers
{
    public class MessageController : Controller
    {
        public object AddMessage(int chatId, int senderId, int recieverId, string messegetext, int? replayMessageId,string token)
        {
            var context = ConnectionService.GetContext();
            var addmess = context.Tokens.Where(am => am.UserId == senderId).FirstOrDefault();
            
                Message message = new Message()
                {

                    ChatId = chatId,
                    SenderId = senderId,
                    RecieverId = recieverId,
                    MessageText = messegetext,
                    ReplayTo = replayMessageId

                };
            if (addmess != null && addmess.TokenId == token)
            {
                context.Messages.Add(message);
                context.SaveChanges();
                return new { error=false,messageId=message.Id};
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
        
        public object DeleteMessage(int senderId,int messageId,string token)
        {
            var context = ConnectionService.GetContext();
            var delmess = context.Tokens.Where(dm => dm.UserId == senderId).FirstOrDefault();
            Message message = context.Messages.Where(del => del.Id == messageId).FirstOrDefault();
            if(delmess!=null&&message?.SenderId == senderId&&delmess.TokenId==token)
            {
                context.Messages.Remove(message);
                context.SaveChanges();
                return new { error = false, messageId = message.Id };
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };

        }
        public object EditMessage(int messageId, int senderId, string messegetext,string token)
        {
            var context = ConnectionService.GetContext();

            var editmess = context.Tokens.Where(em => em.UserId == senderId).FirstOrDefault();
            Message message = context.Messages.Where(e => e.Id == messageId).FirstOrDefault();

            if(editmess!=null&&message?.SenderId == senderId&&editmess.TokenId==token)
            {
                message.MessageText = messegetext;
                context.Messages.Update(message);
                context.SaveChanges();
                return new { error = false, messageId = message.Id };
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
        public object getCountMessages(int chatId)
        {
            var context = ConnectionService.GetContext();
         
            Chat chat = context.Chats.Where(cm => cm.Id == chatId).FirstOrDefault();
            if(chat != null)
            {
                int count = context.Messages.Where(c => c.ChatId == chatId).Count();
                return new { error = false, count=count };
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
        public object getOneMessage(int userId,int messageId,string token)
        {
            var context = ConnectionService.GetContext();
            var getone = context.Tokens.Where(go => go.UserId == userId).FirstOrDefault();
            Message message = context.Messages.Where(mes => mes.Id == messageId).FirstOrDefault();
            if (message != null&&getone!=null&&getone.TokenId==token)
            {
                UserChat userChat = context.UserChats.Where(ch => ch.ChatId == message.ChatId && ch.UserId == userId).FirstOrDefault();
                if(userChat != null)
                {
                    return new { error = false, message =message};
                }

            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
        public object getAllMessages(int numberChat, int recieverid, int startMessegeId, int count,string token)
        {
            var context = ConnectionService.GetContext();
            var allmess = context.Tokens.Where(allm => allm.UserId == recieverid).FirstOrDefault();
            if (allmess != null && allmess.TokenId == token) { 
            var mess = (from allMessage in context.Messages
                        where allMessage.ChatId == numberChat
                        && allMessage.SenderId != recieverid && allMessage.Id >= startMessegeId
                        select allMessage).Take(count);
                return new { error = false, message = mess.ToString() };
            }
            return new { error = true, messageError = "Указанный Вами токен не подходит" };
        }
        public object getMessageId(int chatId)
        {
            var context = ConnectionService.GetContext();
            var messId = (from allMessChat in context.Messages where allMessChat.ChatId == chatId select allMessChat).Select(m=>m.Id);
            return new {error=false,messageId = messId.ToList() };
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}