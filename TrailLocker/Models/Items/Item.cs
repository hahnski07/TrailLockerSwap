using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrailLocker.Models
{
    public class Item
    {
        public Guid ItemID { get; set; }
        public virtual User owner { get; set; } 
        public virtual Locker locker { get; set; } //not sure if we'll need this

        public string condition { get; set; }
        public string location { get; set; }
        public double weight { get; set; }
        public double volume { get; set; }
        public string color { get; set; }
        public string name { get; set; }
        public String brand { get; set; }
    }
}