using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TPP_MainProject.Models.constants;
using TPP_MainProject.Models.entities;

namespace TPP_MainProject.Models.ViewModels
{
    public class WorkItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
         [Required]
        public String Description { get; set; }
         [Required]
        public DateTime DueDate { get; set; }
         [Required]
        public TaskStatus Status { get; set; }
         [Required]
        public virtual Worker AssignedWorker { get; set; }
        public virtual Project AssignedProject { get; set; }

        public WorkItemViewModel() { }
        public WorkItemViewModel(WorkItem workItem)
        {
            this.Id = workItem.id;
            this.Name = workItem.name;
            this.Description = workItem.description;
            this.DueDate = workItem.dueDate;
            this.Status = workItem.status;
            this.AssignedWorker = workItem.assignedWorker;
            this.AssignedProject = workItem.assignedProject;
        }
    }
}