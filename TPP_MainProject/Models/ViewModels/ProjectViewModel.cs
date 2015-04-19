﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPP_MainProject.Models.constants;
using TPP_MainProject.Models.entities;

namespace TPP_MainProject.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public decimal costs { get; set; }
        public ProjectStatus projectStatus { get; set; }
        public virtual ICollection<WorkItem> tasks { get; set; }
        public virtual ApplicationUser projectManager { get; set; }
        public String nameProjectManager { get; set; }
        public virtual Order order { get; set; }

        public ProjectViewModel() { }
        public ProjectViewModel(Project item)
        {
            this.id = item.id;
            this.name = item.name;
            this.costs = item.costs;
            this.projectStatus = item.projectStatus;
            this.tasks = item.tasks;
            this.projectManager = item.projectManager;
            this.nameProjectManager = item.nameProjectManager;
            this.order = item.order;
        }
    }
}