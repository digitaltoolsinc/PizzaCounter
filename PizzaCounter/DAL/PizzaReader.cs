using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PizzaCounter.Controllers;
using PizzaCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PizzaCounter.DAL
{
    public class PizzaReader
    {
        public List<Pizza> Pizzas = new List<Pizza>();

        private IConfiguration _configuration;
        private ILogger<ToppingsController> _logger;

        public PizzaReader(IConfiguration configuration, ILogger<ToppingsController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public List<Pizza> GetPizzas()
        {
            string json = GetJSONString();
            return Deserialize(json);             
            
        }
        
        public string GetJSONString()
        {
            string resultString = string.Empty;
            using (var client = new HttpClient())
            {
                try
                {
                    resultString = client.GetAsync(_configuration["PizzaURI"]).Result.Content.ReadAsStringAsync().Result;
                }
                catch(Exception e)
                {
                    //Log here to your favorite logger, maybe email important people that something went wrong
                    _logger.LogError(e.Message);
                }
            }
            return resultString;
        }

        public List<Pizza> Deserialize(string json)
        {
            List<Pizza> pizzas = new List<Pizza>();
            try
            {
                pizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
            return pizzas;
        }
    }
}
