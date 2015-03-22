using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPP_MainProject.Models.entities
{
    public class TemplateOrder : Order
    {
        public virtual ICollection<ProductItem> orderItems { get; set; }
    }
}