using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace TropicalServerApp.Models
{
    public class OrderDetail
    {
        //[Key]
        //public int OrderID { get; set; }

        public OrderModels.tblOrder orderDetails { get; set; }

        public OrderModels.tblOrderHistory orderHistoryDetails { get; set; }

        public OrderModels.tblCustomer customerDetails { get; set; }
        

      
    }
}