using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPP_MainProject.Models.entities
{
    public class Worker :UserProfile
    {
        public int id  { get; set; }
        public Double Salary { get; set; }
        public DateTime startWorkDate { get; set; }
        public virtual ICollection<Task> tasks { get; set; }
        

    }
}