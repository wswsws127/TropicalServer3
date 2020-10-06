namespace TropicalServerApp.Models.OrderModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblOrder")]
    public partial class tblOrder
    {
        [Key]
        public int OrderID { get; set; }

        [StringLength(50)]
        [DisplayName("Order Tracking Number")]
        public string OrderTrackingNumber { get; set; }

        [DisplayName("Order Route Number")]
        public int? OrderRouteNumber { get; set; }

        [DisplayName("Order Customer Number")]
        public int? OrderCustomerNumber { get; set; }

        [StringLength(50)]
        public string OrderGroupNumber { get; set; }

        [DisplayName("Order Case Numbers")]
        public int? OrderCaseNumbers { get; set; }

        [DisplayName("Order Item Number")]
        public int? OrderItemNumber { get; set; }

        public decimal? OrderPromoNumber { get; set; }

        [DisplayName("Order Item Quantity")]
        public int? OrderItemQty { get; set; }

        public decimal? ItemUnitPrice { get; set; }

        [DisplayName("Item Total Price")]
        public decimal? ItemTotalPrice { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [DisplayName(" Delivery Date")]
        public DateTime? DeliveryDate { get; set; }

        [DisplayName("User ID")]
        [StringLength(50)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string TabletID { get; set; }

        public DateTime? DataSyncDateTime { get; set; }

        public int? OrderConfirmFlag { get; set; }

        public int? ItemType { get; set; }

        public decimal? PerPoundItemsWeight { get; set; }
    }
}
