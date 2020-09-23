using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebb
{
    public class CheckToken
    {
       public static bool Check(int tokenId,int userId,string token)
        {
            var context = ConnectionService.GetContext();
            var check = context.Tokens.Where(dc => dc.UserId == userId).FirstOrDefault();
            if(check != null&&check.TokenId == token && check.UserId == userId&&check.Id == tokenId)
            {
                return true;
            }
            return false;
            
        }
    }
}
