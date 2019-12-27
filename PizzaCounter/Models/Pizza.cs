using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaCounter.Models
{
    public class Pizza
    {
        public List<string> toppings { get; set; }

        public Pizza()
        {
            toppings = new List<string>();
        }
    }
}
