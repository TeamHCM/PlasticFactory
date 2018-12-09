//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlasticsFactory.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Quantity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quantity()
        {
            this.QuantityNotDetails = new HashSet<QuantityNotDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public double TotalSack { get; set; }
        public double TotalWeight { get; set; }
        public double TotalOuputWeight { get; set; }
        public double TotalInventory { get; set; }
        public string Note { get; set; }
        public double TotalOutputSack { get; set; }
        public bool IsEdit { get; set; }
        public bool isDelete { get; set; }
        public double InventoryNotMonth { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuantityNotDetail> QuantityNotDetails { get; set; }
    }
}
