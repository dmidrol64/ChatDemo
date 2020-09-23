using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebb.Models
{
    public class Profile
    {
        
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Verify { get; set; }
        public int UserId { get; set; }

        public UserData UserData { get; set; }
    }
}
