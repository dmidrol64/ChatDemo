using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebb.Models
{
    public class Message
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int SenderId { get; set; }

        [ForeignKey("User")]
        public int RecieverId { get; set; }

        [ForeignKey("Chat")]
        public int ChatId { get; set; }

        [ForeignKey("Message")]
        public int? ReplayTo { get; set; }

        public string MessageText { get; set; }

        public Chat MessChat { get; set; }

        public UserData MessUser { get; set; }

    }
}
