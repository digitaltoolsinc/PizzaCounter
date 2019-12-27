using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PizzaCounter.BL;
using PizzaCounter.DAL;
using PizzaCounter.ViewModels;

namespace PizzaCounter.Controllers
{
    public class ToppingsController : Controller
    {
        private IConfiguration _configuration;
        private ILogger<ToppingsController> _logger;

        public ToppingsController(IConfiguration configuration, ILogger<ToppingsController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        // GET: Toppings
        public ActionResult Index()
        {
            ToppingsCounter counter = new ToppingsCounter(_configuration, _logger);
            List<KeyValuePair<string,int>> toppingsCount = counter.CountToppings();
            List<ToppingsCount> popularToppings = new List<ToppingsCount>();
            foreach(KeyValuePair<string,int> kvp in toppingsCount)
            {
                popularToppings.Add(new ToppingsCount
                {
                    Name = kvp.Key,
                    Count = kvp.Value
                });
            }
            return View(popularToppings);
        }

     

              
    }
}