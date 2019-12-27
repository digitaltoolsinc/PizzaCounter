using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PizzaCounter.Controllers;
using PizzaCounter.DAL;
using PizzaCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaCounter.BL
{
    public class ToppingsCounter
    {
        private ILogger<ToppingsController> _logger;
        private IConfiguration _configuration;

        public ToppingsCounter(IConfiguration configuration,ILogger<ToppingsController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public List<Pizza> GetPizzas()
        {
            PizzaReader pizzaReader = new PizzaReader(_configuration, _logger);
            return pizzaReader.GetPizzas();
        }

        public List<KeyValuePair<string,int>> CountToppings(int topNumber = 20)
        {
            Dictionary<string, int> toppingsCounts = new Dictionary<string, int>();
            List<Pizza> pizzas = GetPizzas();

            foreach(Pizza p in pizzas)
            {
                foreach (string t in p.toppings)
                {
                    if (toppingsCounts.ContainsKey(t))
                        toppingsCounts[t]++;
                    else
                        toppingsCounts.Add(t, 1);                        
                }
            }
            return toppingsCounts.OrderByDescending(i => i.Value).Take(topNumber).ToList();            
        }            
    }
}
