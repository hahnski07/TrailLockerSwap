using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TrailLocker.Models
{
    public class TrailLockerEntities : DbContext
    {
        public DbSet<User> Users {get;set;}
        public DbSet<Locker> Lockers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }
}