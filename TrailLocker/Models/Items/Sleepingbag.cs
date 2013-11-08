using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrailLocker.Models.Items
{
    public class Sleepingbag : SleepingSystem
    {
        public int tempRating { get; set; }
        public int length { get; set; }
    }
}