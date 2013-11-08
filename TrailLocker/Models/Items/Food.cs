using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrailLocker.Models.Items
{
    public class Food : Item
    {
        private Guid guid;

        public Food()
        {
        }


        public Food(Guid guid)
        {
            // TODO: Complete member initialization
            this.guid = guid;
        }

        public Food(Guid guid,int servings, Boolean requireWater)
        {
            // TODO: Complete member initialization
            this.guid = guid;
            this.servings = servings;
            this.requireWater = requireWater;
        }
        public int servings { get; set; }
        public Boolean requireWater { get; set; }
    }
}