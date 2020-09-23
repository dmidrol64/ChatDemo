using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebb.Models
{
    public class KeyStorage
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Key { get; set; }

        //public bool FormKeep { get; set; }

        public UserData UserData { get; set; }
    }
}
