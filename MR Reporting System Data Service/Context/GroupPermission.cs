//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MR_Reporting_System_Data_Service.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class GroupPermission
    {
        public int id { get; set; }
        public Nullable<int> groupId { get; set; }
        public Nullable<int> permissionCode { get; set; }
        public Nullable<bool> value { get; set; }
    
        public virtual Group Group { get; set; }
    }
}