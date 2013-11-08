using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrailLocker.Models.Items
{
    public class Tent : SleepingSystem
    {
        public int numberOfDoors { get; set; }
        public int squareFeet { get; set; }
    }
}