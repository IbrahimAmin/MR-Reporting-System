//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MR_Reporting_System_Data_Context.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Visit
    {
        public int Id { get; set; }
        public Nullable<int> AgentId { get; set; }
        public Nullable<int> DrugsId { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<int> VisitTo { get; set; }
        public Nullable<System.DateTime> VisitDate { get; set; }
        public Nullable<System.TimeSpan> Duration { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsMorning { get; set; }
        public string Notes { get; set; }
        public Nullable<int> LastEditBy { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Agent Agent1 { get; set; }
        public virtual DefaultList DefaultList { get; set; }
        public virtual Drug Drug { get; set; }
    }
}