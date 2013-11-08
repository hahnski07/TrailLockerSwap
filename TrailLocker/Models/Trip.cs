using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TrailLocker.Models
{
    public class Trip
    {
        //Instance Variables
        public Guid TripID { get; set; }
        public Guid UserID { get; set; } //GUID of trip leader

        public float current_weight { get; set; }
        public float total_capacity { get;  set; }

        public String destination { get; set; }

        public virtual User trip_leader { get; set; }
        public virtual ICollection<User> travellers { get; private set; }

        public virtual ICollection<Item> items { get; private set; }

        
      //  public DateTime start_time { get; private set; }
      //  public DateTime end_time { get; private set; }
        public String weather { get; set; }

        public String description { get; set; }
        
        //Constructor
        public Trip(Guid userID)
        {
            this.UserID = userID;
            this.TripID = Guid.NewGuid();
            this.current_weight = 0;
        }

        public Trip()
        {
            this.TripID = Guid.NewGuid();
            this.current_weight = 0;
            this.destination = "TBD";
        }

        //Methods
        public int add_item(Locker new_item)
        {
            return 1;
        }

        public int remove_item(int id)
        {
            return 1;
        }

        public float calculate_current_weight()
        {
            return 1.0F;
        }

        public float calculate_total_capacity()
        {
            return 1.0F;

        }

        public int find_weather()
        {
            return 1;

        }

        public int add_traveller(User new_traveller)
        {
            return 1;
        }

        public int remove_traveller(int id)
        {
            return 1;
        }

        public int generate_suggestions()
        {
            return 1;
        }
    }


    public class TripDBContext : DbContext
    {
        public DbSet<Trip> Trips {get; set;}
    }

    public class TripTestDBContext : DbContext
    {
        public DbSet<Trip> TripTest { get; set; }
        
    }

}

/*
 * Thoughts
 * -User wanting to create a new trip would probably go through interraction with a controller, not Trip class directly
 * -Probably going to have different 'slots' for different item types. E.g. a sleeping bag slot rather than one big list
 *      - This allows for easy suggestions ("Add a sleeping bag")
 *      
 * - Will interract heavily with locker. Will want to be able to look in locker to grab default equiptment; User will need to be able to move things from locker to trip easily
 * 
 * 
 * Things needed from other classes:
 * 

*/