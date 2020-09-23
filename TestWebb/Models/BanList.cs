using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestWebb.Models
{
    public class BanList
    {
        
        public int UserId { get; set; }

        
        public int ChatId { get; set; }

       public List<UserChat> UserChats { get; set; }
    }
}
