using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPP_MainProject.Models.entities
{
    public class ProductItem
    {
        public int id {get; set;}
        public String name {get; set;}
        public String shortDescription {get; set;}
        public String description {get; set;}
        public Double price { get; set;}
    }
}