using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebb
{
    public class ConnectionService
    {
      static  private ApplicationContext context = null;
    public  static ApplicationContext GetContext()
        {
            if(context == null)
            {
                context = new ApplicationContext();
            }
            return context;
        } 
    }
}
