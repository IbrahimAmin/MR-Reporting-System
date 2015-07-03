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
    
    public partial class DefaultList
    {
        public DefaultList()
        {
            this.Agents = new HashSet<Agent>();
            this.Docotors = new HashSet<Docotor>();
            this.Docotors1 = new HashSet<Docotor>();
            this.Drugs = new HashSet<Drug>();
            this.Hospitals = new HashSet<Hospital>();
            this.Orders = new HashSet<Order>();
            this.Visits = new HashSet<Visit>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public Nullable<int> Action { get; set; }
    
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Docotor> Docotors { get; set; }
        public virtual ICollection<Docotor> Docotors1 { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
        public virtual ICollection<Hospital> Hospitals { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
