using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPP_MainProject.Models.constants;

namespace TPP_MainProject.Models.entities
{
    public class WorkItem
    {
        public int Id  { get; set; }
        public String Name  { get; set; }
        public String Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public virtual ApplicationUser AssignedWorker { get; set; }
        public virtual Project assignedProject { get; set; }
        public decimal Price { get; set; }

    
    }
}