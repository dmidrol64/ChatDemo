using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebb.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public List<User> users { get; set; }
        public int UserId { get; set; }
        public UserData User { get; set; }
        public List<UserChat> UserChats { get; set; }

        public List<Message> Messages { get; set; }
        public Chat()
        {
            UserChats = new List<UserChat>();
        }
    }
}
