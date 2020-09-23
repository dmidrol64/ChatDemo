using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebb.Models
{
    public class UserData
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        public string Login { get; set; }
        public string Password{ get; set; }

        public Profile Profile { get; set; }
        public List<Message> Messages { get; set; }
        public List<UserChat> UserChats { get; set; }
        public Token Token { get; set; }

        public List<KeyStorage> Storage { get; set; }
    }
}
