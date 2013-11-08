using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrailLocker.Models.Items
{
    public class Shoes : Item
    {
        public Boolean waterProof { get; set; }
        public int shoeSize { get; set; }
        public int width { get; set; }
        public String type { get; set; }
        public String gender { get; set; }
        public String material { get; set; }
    }
}