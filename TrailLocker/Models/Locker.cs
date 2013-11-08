using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TrailLocker.Models
{
    public class Locker
    {
        public Guid LockerID { get; set; }

        public virtual Guid UserID { get; set; }
        public virtual User owner { get; set; }

        public virtual ICollection<Item> items { get; set; }

        //stub

        public Locker(Guid UserID)
        {
            this.LockerID = Guid.NewGuid();
            this.UserID = UserID;
        }
        public Locker()
        {
            this.LockerID = Guid.NewGuid();
        }
    }
}