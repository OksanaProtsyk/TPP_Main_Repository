using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPP_MainProject.Models.entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual ProductItem ProductItem { get; set; }

        public virtual Order Order { get; set; }
    }
}