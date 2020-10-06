namespace TropicalServerApp.Models.OrderModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCustomer")]
    public partial class tblCustomer
    {
        [Key]
        public int CustID { get; set; }

        public int? CustNumber { get; set; }

        [StringLength(100)]
        public string CustName { get; set; }

        [StringLength(100)]
        public string CustOfficeAddress1 { get; set; }

        [StringLength(100)]
        public string CustOfficeAddress2 { get; set; }

        [StringLength(50)]
        public string CustOfficeCity { get; set; }

        [StringLength(2)]
        public string CustOfficeState { get; set; }

        public int? CustOfficeZip { get; set; }

        [StringLength(50)]
        public string CustPhoneNumber { get; set; }

        [StringLength(50)]
        public string CustFaxNumber { get; set; }

        public int? CustRouteNumber { get; set; }

        public int? CustSalesRepNumber { get; set; }

        [StringLength(100)]
        public string CustOrderEntryContactName { get; set; }

        public int? CustPromoGroup { get; set; }

        public int? CustPriceGroup { get; set; }

        public int? CustPaymentTermsCode { get; set; }

        public int? CustPaymentType { get; set; }

        [StringLength(100)]
        public string CustBillingAddress1 { get; set; }

        [StringLength(100)]
        public string CustBillingAddress2 { get; set; }

        [StringLength(100)]
        public string CustBillingCity { get; set; }

        [StringLength(2)]
        public string CustBillingState { get; set; }

        public int? CustBillingZip { get; set; }

        [StringLength(10)]
        public string CustRouteVisitWeekDay { get; set; }

        public int? CustSequence { get; set; }
    }
}
