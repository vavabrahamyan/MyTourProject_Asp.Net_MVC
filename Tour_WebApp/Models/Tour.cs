using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour_WebApp.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public Tour()
        {
            Categories = new List<Category>();
        }
    }
}