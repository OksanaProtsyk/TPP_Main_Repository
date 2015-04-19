using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using TPP_MainProject.Models.entities;

namespace TPP_MainProject.Models.constants
{
    public static class Utilts
    {

        private static WorkItem first = new WorkItem
        {
            Name = "Test",
            Description = "Make some work",

        };






        public static ICollection<WorkItem>  GenericTasks(TemplateSiteTypes type)
        {

            ICollection<WorkItem> tasks = new Collection<WorkItem>();
            switch (type)
            {
                case TemplateSiteTypes.Blog:
                    
                    break;
                case TemplateSiteTypes.VisitCard:
                    break;
                case TemplateSiteTypes.Oficial:
                    break;
                case TemplateSiteTypes.Shop:
                    break;
                case TemplateSiteTypes.Amazing:
                    break;
                default:
                    break;
            }
            return tasks;
        }
    }
}