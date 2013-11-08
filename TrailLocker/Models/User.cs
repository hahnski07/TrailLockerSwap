using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrailLocker.Models
{
    public class User : TrailLocker.Controllers.LoginModel
    {
        public Guid UserID { get; set; } //primary key in DB

        public String first_name { get; set; }
        public String last_name { get; set; }

        public String home { get; set; }
        public int maxWeight { get; set; }

        public virtual ICollection<User> friends { get; set; }

        public  Locker locker; //why can't I put virtual??? Do I need to?
        public  virtual ICollection<Trip> trips {get; set;}

        public User(String username, String password, String name, String home, int maxWeight)
        {
            this.UserID = Guid.NewGuid();
            this.first_name = name;
            this.home = home;
            this.maxWeight = maxWeight;
        }

        public User(String first_name, String last_name, String email)
        {
            this.UserID = Guid.NewGuid();
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.locker = new Locker(this.UserID);
            this.locker.owner = this;
        }

        public User()
        {
            this.UserID = Guid.NewGuid();
            this.first_name = "Unknown";
            this.maxWeight = 0;
            this.trips = new List<Trip>();
			this.friends = new List<User>();
        }
    }
}