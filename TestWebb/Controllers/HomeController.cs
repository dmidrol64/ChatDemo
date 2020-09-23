using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestWebb.Models;

namespace TestWebb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public string Hello(string user)
        {
            return $"Hello {user} ";
        }
        public string Calculate(string a, string b, string op)
        {
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b) && string.IsNullOrEmpty(op))
            {
                return "Error";
            }
            double da;
            double db;
            if (!(double.TryParse(a, out da) && double.TryParse(b, out db)))
            {
                return "Error";

            }
            //return $"{da} {db}" ;
            switch (op.ToLower())
            {
                case "add": return $"{da + db}";
                case "sub": return $"{da - db}";
                case "mul": return $"{da * db}";
                case "div": return (db != 0) ? $"{da / db}" : "devision by zero";  
            }
            return "Error";
        }
public  string UnaryOperation(string val, string op)
        {
            if (string.IsNullOrEmpty(val) && string.IsNullOrEmpty(op))
            {
                return "Error";
            }
            double da;
        
            if(!(double.TryParse(val, out da)))
            {
                return "Error";
            }
            switch (op.ToLower())
            {
                case "squad": return $"{Math.Sqrt(da)}";
                case "sin": return $"{Math.Sin(da)}";
                case "cos": return $"{Math.Cos(da)}";
            }
            return "Error";
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
