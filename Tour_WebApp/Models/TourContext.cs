using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tour_WebApp.Models
{
    public class TourContext:DbContext
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}