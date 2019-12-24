//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VKHGDDTNN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.ProImages = new HashSet<ProImage>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string Image { get; set; }
        public string Tag { get; set; }
        public Nullable<short> Status { get; set; }
        public int Ord { get; set; }
        public bool Active { get; set; }
        public bool Index { get; set; }
        public Nullable<int> Like { get; set; }
        public Nullable<int> View { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> Num { get; set; }
        public bool Inventory { get; set; }
        public string PriceRetail { get; set; }
        public string PricePromotion { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> SDate { get; set; }
        public Nullable<System.DateTime> EDate { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
        public Nullable<bool> Priority { get; set; }
        public Nullable<int> IdCategory { get; set; }
        public Nullable<int> IdCategory2 { get; set; }
    
        public virtual ICollection<ProImage> ProImages { get; set; }
    }
}
