using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikeDemoProject.Models
{
    /// <summary>
    /// Base Model for all vehicle classes
    /// </summary>
    public class Vehicle
    {
        public string colour { get; set; }
        public int horsePower { get; set; }
        public double price { get; set; }
        public string name { get; set; }
    }
}
