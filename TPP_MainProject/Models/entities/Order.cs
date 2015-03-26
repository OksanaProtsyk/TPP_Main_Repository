using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPP_MainProject.Models.constants;

namespace TPP_MainProject.Models.entities
{
    public abstract class Order
    {
        public int id { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime completeDate { get; set; }
        public OrderStatus orderStartus { get; set; }
    }
    public class TemplateOrder : Order
    {
        public virtual ICollection<ProductItem> orderItems { get; set; }
    }

    public class CustomOrder : Order
    {
        public String detailDescription { get; set; }
    }
}