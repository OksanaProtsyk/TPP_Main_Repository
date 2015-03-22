﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPP_MainProject.Models.constants;

namespace TPP_MainProject.Models.entities
{
    public class Project
    {
        public int id  { get; set; }
        public String name { get; set; }
        public Double costs { get; set; }
        public ProjectStatus projectStatus { get; set; }
        public virtual ICollection<Task> tasks { get; set; }
        public virtual Manager projectManager { get; set; }
    }
}