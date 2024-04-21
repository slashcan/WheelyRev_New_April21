//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WheelyRev.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int transactionID { get; set; }
        public Nullable<int> productID { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> shopID { get; set; }
        public string productName { get; set; }
        public Nullable<int> productQuantity { get; set; }
        public Nullable<decimal> productPrice { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<System.DateTime> datePurchase { get; set; }
        public string customerContact { get; set; }
        public string customerAddress { get; set; }
        public string shopName { get; set; }
        public Nullable<decimal> customerCash { get; set; }
    
        public virtual Products Products { get; set; }
        public virtual Shops Shops { get; set; }
        public virtual Users Users { get; set; }
    }
}
