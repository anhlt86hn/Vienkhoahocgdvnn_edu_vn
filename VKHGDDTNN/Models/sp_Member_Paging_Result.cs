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
    
    public partial class sp_Member_Paging_Result
    {
        public Nullable<long> RowNum { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> SDate { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
        public Nullable<System.DateTime> EDate { get; set; }
        public int Permiss { get; set; }
        public bool Active { get; set; }
    }
}
