using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrailLocker.Models.Items
{
    public class Hammock : SleepingSystem
    {
        private Guid guid;

        public Hammock()
        {
        }

        public Hammock(Guid guid)
        {
            // TODO: Complete member initialization
            this.guid = guid;
        }
        public int weightCap { get; set; }
    }
}