using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebb.Models
{
    public class UserChat
    {
        public int UserId { get; set; }
        public UserData User { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public BanList BanList { get; set; }
    }
}
