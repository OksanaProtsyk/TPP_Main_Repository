using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPP_MainProject.Models.constants;

namespace TPP_MainProject.Models.entities
{
    public class Task
    {
        public int id  { get; set; }
        public String name  { get; set; }
        public String description { get; set; }
        public DateTime dueDate { get; set; }
        public TaskStatus status { get; set; }
        public virtual Worker assignedWorker { get; set; }
        public virtual Project assignedProject { get; set; }
    
    }
}