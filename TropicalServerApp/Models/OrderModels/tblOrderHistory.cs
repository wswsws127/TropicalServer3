namespace TropicalServerApp.Models.OrderModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblOrderHistory")]
    public partial class tblOrderHistory
    {
        [Key]
        public int OrderID { get; set; }

        public int OrderCustomerNumber { get; set; }

        public int? OrderItemNumber { get; set; }

        public DateTime? OrderDate1 { get; set; }

        public decimal? OrderItemQty1 { get; set; }

        public DateTime? OrderDate2 { get; set; }

        public decimal? OrderItemQty2 { get; set; }

        public DateTime? OrderDate3 { get; set; }

        public decimal OrderItemQty3 { get; set; }

        public DateTime? OrderDate4 { get; set; }

        public decimal? OrderItemQty4 { get; set; }

        public DateTime? OrderDate5 { get; set; }

        public decimal? OrderItemQty5 { get; set; }

        public DateTime? OrderDate6 { get; set; }

        public decimal? OrderItemQty6 { get; set; }
    }
}
